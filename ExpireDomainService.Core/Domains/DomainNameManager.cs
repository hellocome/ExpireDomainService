using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Common.Loader;
using ExpireDomainService.Common.Filter;

namespace ExpireDomainService.Core.Domains
{
    public sealed class DomainNameManager
    {
        public static readonly string GLOBAL = "GLOBAL";
        private Dictionary<string, List<ExpireDomainName>> CacheDictionary = new Dictionary<string, List<ExpireDomainName>>();
        private ILoader<ExpireDomainName> mLoader = null;
        private readonly IList<IFilter<ExpireDomainName>> globalDomainLoadFilter = null;
        private readonly IList<IFilter<ExpireDomainName>> cacheFilter = null;

        public DomainNameManager()
        {
            globalDomainLoadFilter = ServiceConfiguration.Instance.GlobalDomainLoadFilter.AsReadOnly();
            cacheFilter = ServiceConfiguration.Instance.CacheFilter.AsReadOnly();
        }

        public void SetLoader(ILoader<ExpireDomainName> loader)
        {
            mLoader = loader;
        }

        private bool ApplyGlobalDomainLoadFilter(ExpireDomainName item)
        {
            if (item != null)
            {
                if (globalDomainLoadFilter.Count == 0)
                {
                    return true;
                }

                foreach (IFilter<ExpireDomainName> filter in globalDomainLoadFilter)
                {
                    if (filter.Decide(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void BuildCache()
        {
            CacheDictionary.Clear();

            List<ExpireDomainName> global = new List<ExpireDomainName>();

            while (mLoader.HasNext())
            {
                ExpireDomainName domainName = mLoader.Next();

                if (ApplyGlobalDomainLoadFilter(domainName))
                {
                    global.Add(domainName);
                }
            }

            CacheDictionary[GLOBAL] = global;
        }
    }
}
