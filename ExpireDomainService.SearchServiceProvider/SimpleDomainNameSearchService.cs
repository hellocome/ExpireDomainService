using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Core.SearchService;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Common.Collection;
using ExpireDomainService.SearchServiceProvider.Cache;
using ExpireDomainService.Common.Extensions;

namespace ExpireDomainService.SearchServiceProvider
{
    public sealed class SimpleDomainNameSearchService : IDomainNameSearchService
    {
        public static readonly string GLOBAL = "GLOBAL";
        private Dictionary<string, PagedSet<ExpireDomainName>> mSearchData = null;
        private SortedCacheContainer<string, List<ExpireDomainName>> container;

        public SimpleDomainNameSearchService(int cacheSize)
        {
            container = new SortedCacheContainer<string, List<ExpireDomainName>>(cacheSize);
        }

        public void UpdateBaseData(Dictionary<string, PagedSet<ExpireDomainName>> searchData)
        {
            // this is already a shadow copy
            mSearchData = searchData;
        }


        private List<ExpireDomainName> search(string collectionName, string keyWord, int page)
        {
            string collectionPart = collectionName;
            string keyWordPart = keyWord;
            int pagePart = page;
            
            if (string.IsNullOrEmpty(collectionName) || !mSearchData.ContainsKey(collectionName))
            {
                collectionPart = GLOBAL;
            }

            ExpireDomainName[] pageSet = mSearchData[collectionName].GetPage(page);

            if (string.IsNullOrEmpty(keyWordPart))
            {
                keyWordPart = keyWordPart.RemoveSpace().ToLower();
            }

            
            if (pageSet != null)
            {
                string dicKey = buildKey(collectionName, keyWord, page);
            }
            else
            {

            }

            return null;
        }


        public List<ExpireDomainName> SearchText(string keyWord, int page)
        {
            string key = buildKey("", keyWord, page);
            ICacheItem<string, List<ExpireDomainName>> item = null;

            if (container.TryGetValue(key, out item))
            {
                return item.CacheItemValue;
            }
            else
            {
               return 
            }
        }

        public List<ExpireDomainName> SearchText(string collectionName, string keyWord, int page)
        {
        }

        public List<ExpireDomainName> SearchRegex(string regex, int page)
        {

        }



        public List<ExpireDomainName> SearchRegex(string collectionName, string keyWord, int page)
        {
        }


        public List<ExpireDomainName> ReturnPage(string collectionName, int page)
        {
        }


        public List<ExpireDomainName> ReturnPage(int page)
        {
        }

        private static string buildKey(string collectionName, string keyWord, int page)
        {
            return string.Format("{0}#{1}#{2}", collectionName, keyWord, page);
        }

    }
}
