using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Core.Domains;

namespace ExpireDomainService.SearchServiceProvider.Cache
{
    public sealed class SearchResultCacheItem : ICacheItem<string, List<ExpireDomainName>>
    {
        public string CacheItemKey
        {
            get;
            private set;
        }

        public List<ExpireDomainName> CacheItemValue
        {
            get;
            private set;
        }

        public SearchResultCacheItem(string key)
        {
            CacheItemKey = key;
            CacheItemValue = new List<ExpireDomainName>();
        }

        public SearchResultCacheItem(string key, List<ExpireDomainName> cacheValue)
        {
            CacheItemKey = key;
            CacheItemValue = cacheValue;
        }
    }
}
