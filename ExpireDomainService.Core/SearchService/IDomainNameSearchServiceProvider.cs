using System;
using System.Collections.Generic;
using System.ServiceModel;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Common.Collection;

namespace ExpireDomainService.Core.SearchService
{
    public interface IDomainNameSearchServiceProvider
    {
        void UpdateSearchData(Dictionary<string, PagedSet<ExpireDomainName>> searchData);

        IDomainNameSearchService DomainNameSearchService { get; }
    }
}
