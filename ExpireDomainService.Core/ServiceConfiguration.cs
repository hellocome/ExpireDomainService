using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExpireDomainService.Common.Reflection;

namespace ExpireDomainService.Common.Logging
{
    public class ServiceConfiguration
    {
        public static readonly string CONFIGURATION_FILE = "ServiceConfiguration.xml";
        private static ServiceConfiguration instance = new ServiceConfiguration();

        public static ServiceConfiguration Instance
        {
            get
            {
                return instance;
            }
        }

        private ServiceConfiguration()
        {
            Load();
        }

        private void Load()
        {
            try
            {

                string localConfiguration = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + CONFIGURATION_FILE;
                XmlDocument doc = new XmlDocument();
                doc.Load(localConfiguration);

                XmlElement ele = (doc.SelectSingleNode("Configuration/Logger") as XmlElement);

                string moduleName = ele.GetAttribute("module");
                string implClass = ele.GetAttribute("class");

                if (string.IsNullOrEmpty(moduleName))
                {
                    Logger = ObjectHelper.Create<ILogger>(implClass);
                }
                else
                {
                    Logger = ObjectHelper.Create<ILogger>(moduleName, implClass);
                }

                IsDebugOn = ele.GetAttribute("IsDebugOn").Equals("true", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                IsDebugOn = false;
            }
        }

        private List<ICheckPoint> GetCheckPoint()
        {
            StringBuilder strBuilder = new StringBuilder();

            List<ICheckPoint> checkPoints = new List<ICheckPoint>();
            string pathWeekDay = "Scheduler\\WeekDayCheckPoint";
            string pathRunOnceCheckPoint = "Scheduler\\RunOnceCheckPoint";
            List<string> names = RegistryWrapper.EnumerateRegName(Registry.LocalMachine, string.Format("{0}\\Scheduler\\WeekDayCheckPoint", ROOT));

            foreach (string name in names)
            {
                try
                {
                    string hm = GetValueString(pathWeekDay, name);
                    ICheckPoint checkPoint = new WeekDayCheckPoint(name, hm);

                    checkPoints.Add(checkPoint);

                    strBuilder.AppendLine(checkPoint.ToString());
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error("Failed to load check point: ", ex);
                    Logger.Instance.Error("CheckPoing: " + pathWeekDay + "/" + name);
                }
            }

            if (GetValueString(pathRunOnceCheckPoint, "RunOnServiceStartCheckPoint", "").Equals("1"))
            {
                RunOnServiceStartCheckPoint runOnServiceStarts = new RunOnServiceStartCheckPoint();
                checkPoints.Add(runOnServiceStarts);
                strBuilder.AppendLine(runOnServiceStarts.ToString());
            }

            Logger.Instance.Info(strBuilder.ToString());

            return checkPoints;
        }
    }
}
