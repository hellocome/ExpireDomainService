using System;
using System.Collections.Generic;
using ExpireDomainService.Common.WCF;
using ExpireDomainService.WCF;

namespace ExpireDomainService
{
    public sealed class ExpireDomainServiceManager : WCFServiceManager
    {
        private static ExpireDomainServiceManager mInstance = new ExpireDomainServiceManager();

        public static ExpireDomainServiceManager Instance
        {
            get
            {
                return mInstance;
            }
        }
        
        protected override void LoadServices()
        {
            mServices.Add(new ExpireDomainWCFService());
        }
    }
}
