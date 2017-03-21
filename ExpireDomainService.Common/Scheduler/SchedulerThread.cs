using System;
using System.Threading;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Common.Schedule
{
    public abstract class SchedulerThread
    {
        private readonly int checkInterval;
        protected Thread thread = null;
        protected volatile bool KeepRunning = true;
        protected EventWaitHandle mWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        protected object locker = new object();

        public SchedulerThread(int interval)
        {
            checkInterval = interval;
            thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = false;
        }

        // we can only start the thread once, otherwise exception will be thrown
        public void Start()
        {
            try
            {
                Logger.Instance.Info(string.Format("Starting DHLParcelShopFinder Scheduler thread..."));

                thread.Start();

                Logger.Instance.Info(string.Format("DHLParcelShopFinder Scheduler thread is started!"));
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Failed to start DHLParcelShopFinder Scheduler thread", ex);
            }
        }

        public void Stop()
        {
            try
            {
                Logger.Instance.Info("--> SchedulerThread.Stop()");
                lock (locker)
                {
                    KeepRunning = false;
                    Monitor.Pulse(locker);
                }

                Logger.Instance.Info("--> SchedulerThread.Stop.Wait");
                if (!mWaitHandle.WaitOne(25 * 1000))
                {
                    Logger.Instance.Info("--> SchedulerThread.Stop.WaitTimeout");
                    Environment.Exit(0);
                }

                Logger.Instance.Info("--> SchedulerThread.Stop.WaitOK");
                Logger.Instance.Info("<-- SchedulerThread.Stop()");
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
            }
        }

        private void Run()
        {
            try
            {
                while (KeepRunning)
                {
                    try
                    {
                        Logger.Instance.Debug("--> SchedulerThread.Run()");
                        Execute();
                        Logger.Instance.Debug("<-- SchedulerThread.Run()");
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Error(ex.ToString());
                    }
                    
                    lock (locker)
                    {
                        if (KeepRunning)
                        {
                            Logger.Instance.Debug("--> SchedulerThread.Sleep()");
                            Monitor.Wait(locker, TimeSpan.FromMilliseconds(checkInterval));
                            Logger.Instance.Debug("--> SchedulerThread.Sleep.Done()");
                        }
                    }
                }
            }
            finally
            {
                Logger.Instance.Info("--> SchedulerThread.Run.Signal");
                // We have finished.
                mWaitHandle.Set();
            }
        }

        protected abstract void Execute();

    }
}
