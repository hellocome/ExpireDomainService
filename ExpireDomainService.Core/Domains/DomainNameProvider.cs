using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Common.Loader;
using ExpireDomainService.Common.Filter;
using ExpireDomainService.Common.Collection;
using ExpireDomainService.Common.Logging;

using ExpireDomainService.Common.Patterns;

namespace ExpireDomainService.Core.Domains
{
    public sealed class DomainNameProvider : AbstractObserable<Dictionary<string, PagedSet<ExpireDomainName>>>
    {
        public static readonly string GLOBAL = "GLOBAL";
        private Dictionary<string, PagedSet<ExpireDomainName>> CacheDictionary = new Dictionary<string, PagedSet<ExpireDomainName>>();
        private ILoader<ExpireDomainName> mLoader = null;
        private readonly IList<IFilter<ExpireDomainName>> globalDomainLoadFilters = null;
        private readonly IList<IFilter<ExpireDomainName>> cacheFilters = null;
   
        public DomainNameProvider()
        {
            globalDomainLoadFilters = ServiceConfiguration.Instance.GlobalDomainLoadFilter.AsReadOnly();
            cacheFilters = ServiceConfiguration.Instance.CacheFilter.AsReadOnly();
        }

        public void SetLoader(ILoader<ExpireDomainName> loader)
        {
            mLoader = loader;
        }

        public void BuildCache()
        {
            try
            {
                CacheDictionary.Clear();

                PagedSet<ExpireDomainName> global = new PagedSet<ExpireDomainName>();

                foreach (IFilter<ExpireDomainName> cacheFilter in cacheFilters)
                {
                    if (!CacheDictionary.ContainsKey(cacheFilter.UID))
                    {
                        CacheDictionary[cacheFilter.UID] = new PagedSet<ExpireDomainName>();
                    }
                }

                while (mLoader.HasNext())
                {
                    ExpireDomainName domainName = mLoader.Next();

                    if (domainName != null)
                    {
                        if (ApplyGlobalDomainLoadFilter(domainName))
                        {
                            global.Add(domainName);

                            // Only when global accept the domain
                            foreach (IFilter<ExpireDomainName> cacheFilter in cacheFilters)
                            {
                                if (cacheFilter.Decide(domainName))
                                {
                                    CacheDictionary[cacheFilter.UID].Add(domainName);
                                }
                            }
                        }
                    }
                }

                CacheDictionary[GLOBAL] = global;


                foreach (string uid in CacheDictionary.Keys)
                {
                    Logger.Instance.Info("Cache [{0}] = {1}", uid, CacheDictionary[uid].Count);
                }

                NotifyObserver(CacheDictionary);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error in building Cache", ex);
                NotifyObserver(ex);
            }
        }

        public void Complete()
        {
            NotifyObserverComplete();
        }

        private void NotifyObserver(Exception ex)
        {
            foreach(var observer in observers)
            {
                observer.OnError(ex);
            }
        }

        private void NotifyObserverComplete()
        {
            foreach (var observer in observers)
            {
                observer.OnCompleted();
            }
        }

        private void NotifyObserver(Dictionary<string, PagedSet<ExpireDomainName>> cacheDictionary)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(cacheDictionary);
            }
        }

        private bool ApplyGlobalDomainLoadFilter(ExpireDomainName item)
        {
            if (item != null)
            {
                if (globalDomainLoadFilters.Count == 0)
                {
                    return true;
                }

                foreach (IFilter<ExpireDomainName> filter in globalDomainLoadFilters)
                {
                    if (filter.Decide(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
