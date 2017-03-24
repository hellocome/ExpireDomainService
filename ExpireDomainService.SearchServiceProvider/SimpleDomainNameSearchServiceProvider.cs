using System;
using System.Collections.Generic;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Common.Collection;
using ExpireDomainService.Core.SearchService;

namespace ExpireDomainService.SearchServiceProvider
{
    public sealed class SimpleDomainNameSearchServiceProvider : IDomainNameSearchServiceProvider
    {
        private static SimpleDomainNameSearchService searchService = new SimpleDomainNameSearchService();

        public void UpdateSearchData(Dictionary<string, PagedSet<ExpireDomainName>> searchData)
        {
            searchService.UpdateBaseData(searchData);
        }

        public IDomainNameSearchService DomainNameSearchService
        {
            get
            {
                return searchService;
            }
        }
    }
}
