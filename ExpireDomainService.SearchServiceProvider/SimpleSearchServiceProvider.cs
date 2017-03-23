using System;
using System.Collections.Generic;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Common.Collection;
using ExpireDomainService.Core.SearchService;

namespace ExpireDomainService.SearchServiceProvider
{
    public sealed class SimpleSearchServiceProvider : IDomainNameSearchServiceProvider
    {
        public void UpdateSearchData(Dictionary<string, PagedSet<ExpireDomainName>> searchData)
        {
        }

        public IDomainNameSearchService DomainNameSearchService
        {
            get
            {
                return null;
            }
        }
    }
}
