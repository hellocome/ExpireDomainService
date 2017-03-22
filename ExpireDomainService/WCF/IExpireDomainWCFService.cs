using System;
using System.Collections.Generic;
using System.ServiceModel;
using ExpireDomainService.Core.Domains;

namespace ExpireDomainService.WCF
{
    [ServiceContract(Namespace = Constants.Namespace)]
    public interface IExpireDomainWCFService
    {
        [OperationContract]
        List<ExpireDomainName> SearchText(string keyWord, int page);

        [OperationContract]
        List<ExpireDomainName> SearchRegex(string regex, int page);

        [OperationContract]
        List<ExpireDomainName> SearchText(string collectionName, string keyWord, int page);

        [OperationContract]
        List<ExpireDomainName> SearchRegex(string collectionName, string keyWord, int page);

        [OperationContract]
        List<ExpireDomainName> ReturnPage(string collectionName, int page);

        [OperationContract]
        List<ExpireDomainName> ReturnPage(int page);
    }
}
