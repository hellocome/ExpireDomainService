using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExpireDomainService.Common.Schedule;
using ExpireDomainService.Common.Logging;
using ExpireDomainService.Common.Reflection;
using ExpireDomainService.Common.Filter;
using ExpireDomainService.Common.Loader;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Core.SearchService;

namespace ExpireDomainService.Core
{
    public class SimpleDomainNameSearchServiceProviderConfiguration
    {
        public static readonly string GLOBAL = "GLOBAL";
        public static readonly string CONFIGURATION_FILE = "ServiceConfiguration.xml";
        private IDomainNameSearchService domainNameSearchService;

        private class Holder
        {
            public static SimpleDomainNameSearchServiceProviderConfiguration instance = new SimpleDomainNameSearchServiceProviderConfiguration();

            static Holder()
            {
                instance.LoadConfiguration();
            }
        }

        public static SimpleDomainNameSearchServiceProviderConfiguration Instance
        {
            get
            {
                return Holder.instance;
            }
        }

        private SimpleDomainNameSearchServiceProviderConfiguration()
        {
        }

        private void LoadConfiguration()
        {
            try
            {

                string localConfiguration = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + CONFIGURATION_FILE;
                XmlDocument doc = new XmlDocument();
                doc.Load(localConfiguration);

                LoadDomainLoader(doc);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Fail to LoadConfiguration", ex);
            }
        }

        private void LoadDomainLoader(XmlDocument doc)
        {
            try
            {

                XmlElement ele = doc.SelectSingleNode("Configuration/SearchServiceProvider/DomainNameSearchService") as XmlElement;

                String sModule = ele.GetAttribute("module");
                String sClass = ele.GetAttribute("class");
                String scacheSize = ele.GetAttribute("cacheSize");

                domainNameSearchService = ObjectHelper.Create<IDomainNameSearchService>(sModule, sClass, scacheSize);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Fail to LoadConfiguration", ex);
            }
        }

        public IDomainNameSearchService DomainNameSearchService
        {
            get
            {
                return domainNameSearchService;
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
