using System;
using System.Collections.Generic;
using ExpireDomainService.Common.Collection;
using ExpireDomainService.Common.Logging;
using ExpireDomainService.Common.Patterns;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Core.SearchService;

namespace ExpireDomainService.Core.Data
{
    public sealed class DomainNameServiceAdapter : AbstractObserver<Dictionary<string, PagedSet<ExpireDomainName>>>
    {
        private Dictionary<string, PagedSet<ExpireDomainName>> CacheDictionary = new Dictionary<string, PagedSet<ExpireDomainName>>();
        private IDomainNameSearchServiceProvider searchServiceProvider = null;
        public IDomainNameSearchServiceProvider SearchServiceProvider
        {
            get
            {
                return searchServiceProvider;
            }
        }

        private static DomainNameServiceAdapter instance = new DomainNameServiceAdapter();
        public static DomainNameServiceAdapter Instance
        {
            get
            {
                return instance;
            }
        }

        private DomainNameServiceAdapter()
        {

        }

        private void UpdateCacheDictionary(Dictionary<string, PagedSet<ExpireDomainName>> cacheDictionary)
        {
            lock (CacheDictionary)
            {
                CacheDictionary.Clear();
                
                foreach(string key in cacheDictionary.Keys)
                {
                    CacheDictionary[key] = cacheDictionary[key];
                }

                searchServiceProvider.UpdateSearchData(CacheDictionary);
            }
        }

        public override void OnError(Exception error)
        {
            Logger.Instance.Error("Error in Domain Name provider", error);
        }

        public override void OnNext(Dictionary<string, PagedSet<ExpireDomainName>> value)
        {
            if (value != null)
            {
                UpdateCacheDictionary(value);
            }
        }

        public void SetDomainNameSearchServiceProvider(IDomainNameSearchServiceProvider serviceProvider)
        {
            lock (CacheDictionary)
            {
                searchServiceProvider = serviceProvider;
                searchServiceProvider.UpdateSearchData(CacheDictionary);
            }
        }
    }
}
