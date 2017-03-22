using System;
using System.Configuration.Install;
using System.Collections;
using System.Threading;

#if DEBUG
using System.Windows.Forms;
#else
using System.ServiceProcess;
#endif

using ExpireDomainService.Services;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService
{
    static class Program
    {
        private static Mutex mMutex = new Mutex(false, "ExpireDomainService.Running");
        private static EventWaitHandle mEventExit = new EventWaitHandle(false, EventResetMode.ManualReset, "ExpireDomainService.Exit");

        public static bool IsRunning
        {
            get
            {
                try
                {
                    bool acquired = mMutex.WaitOne(TimeSpan.Zero, true);
                    if (acquired)
                    {
                        mMutex.ReleaseMutex();
                        return false;
                    }
                }
                catch (AbandonedMutexException)
                {
                    return false;
                }

                return true;
            }
        }

        public static bool AcquireMutex()
        {
            return mMutex.WaitOne(TimeSpan.Zero, true);
        }

        public static void RunAsApplication()
        {
            try
            {
                new ExpireDomainWinService().StartService();

                mEventExit.Reset();
                mEventExit.WaitOne();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.Info("ExpireDomainService.MainApp.RunAsApplication() - Caught exception: " + ex);
            }
        }

        static void ApplicationExit(object sender, EventArgs e)
        {
            mEventExit.Set();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                string opt = null;
                if (args.Length >= 1)
                {
                    opt = args[0];
                }

                if (opt != null && opt.ToLower() == "/install")
                {
                    Logger.Instance.Info("Installing ExpireDomainWinService service...");

                    try
                    {
                        TransactedInstaller ti = new TransactedInstaller();
                        ExpireDomainWinServiceInstaller mi = new ExpireDomainWinServiceInstaller();
                        ti.Installers.Add(mi);
                        string path = string.Format("/assemblypath={0}",
                        System.Reflection.Assembly.GetExecutingAssembly().Location);
                        string[] cmdline = { path };
                        InstallContext ctx = new InstallContext("ServiceInstall.log", cmdline);
                        ti.Context = ctx;
                        ti.Install(new Hashtable());
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Error("Service installation failed ... " + e.ToString());
                    }

                    Logger.Instance.Info("Installed service successfully");
                    return;
                }
                else if (opt != null && opt.ToLower() == "/uninstall")
                {
                    try
                    {
                        Logger.Instance.Info("Un-Installing ExpireDomainWinService service...");
                        TransactedInstaller ti = new TransactedInstaller();
                        ExpireDomainWinServiceInstaller mi = new ExpireDomainWinServiceInstaller();
                        ti.Installers.Add(mi);
                        string path = string.Format("/assemblypath={0}",
                        System.Reflection.Assembly.GetExecutingAssembly().Location);
                        string[] cmdline = { path };
                        InstallContext ctx = new InstallContext("", cmdline);
                        ti.Context = ctx;
                        ti.Uninstall(null);
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Error("Service ExpireDomainWinService uninstall failed ... " + e.ToString());
                    }

                    Logger.Instance.Info("Un-Installed service successfully");
                    return;
                }
                else
                {
                    if (!AcquireMutex())
                    {
                        Logger.Instance.Info("An instance of ExpireDomainWinService is already running. Terminating.");
                        return;
                    }
#if DEBUG
                    Thread program = new Thread(RunAsApplication);
                    program.SetApartmentState(ApartmentState.MTA);
                    program.Start();

                    Application.ApplicationExit += new EventHandler(ApplicationExit);
                    Application.Run();


                    mMutex.ReleaseMutex();
#else
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[] { new ExpireDomainWinService() };

                    ServiceBase.Run(ServicesToRun);
#endif

                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Init ExpireDomainWinService Failed!");
                Logger.Instance.Error(ex.ToString());
            }
        }
    }
}
