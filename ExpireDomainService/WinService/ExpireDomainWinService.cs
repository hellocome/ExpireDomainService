using System;
using System.ServiceProcess;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Services
{
    public class ExpireDomainWinService : ServiceBase
    {
        public ExpireDomainWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (args != null && args.Length>= 1 && (args[0].ToLower() == "/d" || args[0].ToLower() == "-d"))
            {
                Logger.Instance.Info("DebugMode: " + args[0]);
                Logger.Instance.SetDebugMode(true);
            }

            Logger.Instance.Info("DebugMode: " + Logger.Instance.IsDebugOn);

            StartService();
        }

        public void StartService()
        {
            try
            {
                Logger.Instance.Info("--> ExpireDomainWinService:StartService");

                ExpireDomainServiceManager.Instance.Start();

                Logger.Instance.Info("<-- ExpireDomainWinService:StartService");
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Can't Start Service", ex);
            }
        }

        protected override void OnStop()
        {
            Logger.Instance.Info("--> ExpireDomainWinService is Stopping...");

            ExpireDomainServiceManager.Instance.Stop();

            Logger.Instance.Info("<-- ExpireDomainWinService is stopped");
        }



        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "ExpireDomainService";
        }

        #endregion
    }
}
