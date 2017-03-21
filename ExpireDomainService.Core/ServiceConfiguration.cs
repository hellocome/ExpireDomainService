using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExpireDomainService.Common.Schedule;
using ExpireDomainService.Common.Logging;
using ExpireDomainService.Common.Reflection;

namespace ExpireDomainService.Core
{
    public class ServiceConfiguration
    {
        public static readonly string CONFIGURATION_FILE = "ServiceConfiguration.xml";
        private static ServiceConfiguration instance = new ServiceConfiguration();
        private static List<ICheckPoint> checkPoints = new List<ICheckPoint>();
        private static int checkInterval;

        public static ServiceConfiguration Instance
        {
            get
            {
                return instance;
            }
        }

        private ServiceConfiguration()
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            try
            {

                string localConfiguration = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + CONFIGURATION_FILE;
                XmlDocument doc = new XmlDocument();
                doc.Load(localConfiguration);

                LoadSchedule(doc);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Fail to LoadConfiguration", ex);
            }
        }


        private void LoadSchedule(XmlDocument doc)
        {
            try
            {
                checkPoints.Clear();

                checkInterval = GetValueInt(doc, "Configuration/Scheduler", "checkInterval", 60, 20, 120);

       
                XmlNodeList nodeList = doc.SelectNodes("Configuration/Scheduler/CheckPoints/CheckPoint");

                foreach (XmlNode node in nodeList)
                {
                    XmlElement ele = node as XmlElement;
                    String sModule = ele.GetAttribute("module");
                    String sClass = ele.GetAttribute("class");
                    String sParameter = ele.GetAttribute("parameter");

                    Logger.Instance.Info("Loading: Module={0} Class={1} Parameter={2}", sModule, sClass, sParameter);

                    ICheckPoint checkPoint = ObjectHelper.Create<ICheckPoint>(sModule, sClass, sParameter);

                    if (checkPoint != null)
                    {
                        checkPoints.Add(checkPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Fail to LoadSchedule", ex);
            }
        }

        public List<ICheckPoint> CheckPoints
        {
            get
            {
                return checkPoints;
            }
        }

        public int CheckInterval
        {
            get
            {
                return checkInterval;
            }
        }

        #region Helper
        private static int GetValueInt(XmlDocument xmlDoc, string path, string attribute, int defaultValue, int min, int max)
        {
            int ret = defaultValue;

            try
            {
                XmlElement pathEle = xmlDoc.SelectSingleNode(path) as XmlElement;

                if (pathEle != null)
                {
                    string attValue = pathEle.GetAttribute(attribute);

                    if (!(int.TryParse(attValue, out ret) && ret >= min && ret <= max))
                    {
                        ret = defaultValue;
                    }
                }
            }
            catch (Exception)
            {
                ret = defaultValue;
            }

            return ret;
        }

        private static string GetValueString(XmlDocument xmlDoc, string path, string attribute, string defaultValue = "")
        {
            string ret = defaultValue;

            try
            {
                XmlElement pathEle = xmlDoc.SelectSingleNode(path) as XmlElement;

                if (pathEle != null)
                {
                    string attValue = pathEle.GetAttribute(attribute);

                    if (!string.IsNullOrEmpty(attValue))
                    {
                        ret = attValue;
                    }
                }
            }
            catch (Exception)
            {
                ret = defaultValue;
            }

            return ret;
        }
        #endregion
    }
}
