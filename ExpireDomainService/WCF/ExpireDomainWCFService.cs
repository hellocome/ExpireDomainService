using System;
using System.Collections.Generic;
using System.ServiceModel;
using ExpireDomainService.Common.WCF;
using ExpireDomainService.Core.Domains;

namespace ExpireDomainService.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ExpireDomainWCFService : WcfService, IExpireDomainWCFService
    {
        public override void Init()
        {
            m_Initialised = true;
        }

        public List<ExpireDomainName> SearchText(string keyWord, int page)
        {
            throw new NotImplementedException();
        }

        public List<ExpireDomainName> SearchRegex(string regex, int page)
        {
            throw new NotImplementedException();
        }

        public List<ExpireDomainName> SearchText(string collectionName, string keyWord, int page)
        {
            throw new NotImplementedException();
        }

        public List<ExpireDomainName> SearchRegex(string collectionName, string keyWord, int page)
        {
            throw new NotImplementedException();
        }

        public List<ExpireDomainName> ReturnPage(string collectionName, int page)
        {
            throw new NotImplementedException();
        }

        public List<ExpireDomainName> ReturnPage(int page)
        {
            throw new NotImplementedException();
        }
    }
}
