using System;
using System.Collections.Generic;
using System.ServiceModel;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Common.Collection;

namespace ExpireDomainService.Core.SearchService
{
    public interface IDomainNameSearchService
    {
        void UpdateBaseData(Dictionary<string, PagedSet<ExpireDomainName>> searchData);
       
        List<ExpireDomainName> SearchText(string keyWord, int page);
        
        List<ExpireDomainName> SearchRegex(string regex, int page);
        
        List<ExpireDomainName> SearchText(string collectionName, string keyWord, int page);
        
        List<ExpireDomainName> SearchRegex(string collectionName, string keyWord, int page);
        
        List<ExpireDomainName> ReturnPage(string collectionName, int page);
        
        List<ExpireDomainName> ReturnPage(int page);
    }
}
