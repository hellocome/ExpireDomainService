using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Common.WCF
{
    public abstract class WCFServiceManager
    {
        protected List<IWcfService> mServices = new List<IWcfService>();

        protected virtual bool InitialiseService(IWcfService service)
        {
            Logger.Instance.Info("Initialising service: " + service);

            try
            {
                service.Init();
            }
            catch (Exception ex)
            {
                Logger.Instance.Info("Caught exception initialising service " + service + ": " + ex);
            }

            if (service.Initialised)
            {
                Logger.Instance.Info("Initialised service ok.");
            }
            else
            {
                Logger.Instance.Info("Service " + service + " failed to initialise.");
            }

            return service.Initialised;
        }

        protected abstract void LoadServices();

        public virtual void Start()
        {
            LoadServices();

            Dictionary<string, string> initSummary = new Dictionary<string, string>();
            Dictionary<string, double> initTimings = new Dictionary<string, double>();

            foreach (IWcfService service in mServices)
            {
                DateTime startTime = DateTime.Now;

                InitialiseService(service);

                DateTime endTime = DateTime.Now;
                double initTime = (float)(endTime - startTime).TotalMilliseconds;
                initTime /= 1000.0;

                initTimings[service.ToString()] = initTime;
                initSummary[service.ToString()] = service.StartupStatus;

                service.OpenService();
            }

            double totalStartupTime = 0.0;
            Logger.Instance.Info("------------------------------ Startup time -------------------------------------");
            foreach (KeyValuePair<string, string> serviceInit in initSummary)
            {
                Logger.Instance.Info(string.Format("- {0,-60} [{1}] [{2:0.00} secs]", serviceInit.Key, serviceInit.Value, initTimings[serviceInit.Key]));
                totalStartupTime += initTimings[serviceInit.Key];
            }

            Logger.Instance.Info(string.Format("Total startup time: [{0:0.00} secs]", totalStartupTime));
        }

        public virtual void Stop()
        {
            Dictionary<string, string> initSummary = new Dictionary<string, string>();
            Dictionary<string, double> initTimings = new Dictionary<string, double>();

            foreach (IWcfService service in mServices)
            {
                DateTime stopTime = DateTime.Now;

                DateTime endTime = DateTime.Now;
                double initTime = (float)(endTime - stopTime).TotalMilliseconds;
                initTime /= 1000.0;

                initTimings[service.ToString()] = initTime;
                initSummary[service.ToString()] = service.StartupStatus;

                service.CloseService();
            }

            double totalShutdownTime = 0.0;
            Logger.Instance.Info("------------------------------ Shutdown time -------------------------------------");
            foreach (KeyValuePair<string, string> serviceInit in initSummary)
            {
                Logger.Instance.Info(string.Format("- {0,-60} [{1}] [{2:0.00} secs]", serviceInit.Key, serviceInit.Value, initTimings[serviceInit.Key]));
                totalShutdownTime += initTimings[serviceInit.Key];
            }

            Logger.Instance.Info(string.Format("Total shutdown time: [{0:0.00} secs]", totalShutdownTime));
        }
    }
}
