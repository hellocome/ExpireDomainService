using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Core.Domains.Filter.CacheFilters
{
    public sealed class LengthRestrictedCacheFilter : DomainNameLengthFilter
    {
        public LengthRestrictedCacheFilter(String parameter) : base(parameter)
        {
            Logger.Instance.Info("LengthRestrictedCacheFilter.Parameters = {0}", parameter);

            foreach (string dName in this.StringParameters)
            {
                Logger.Instance.Info("LengthRestrictedCacheFilter domain will be load = {0}", dName);
            }
        }
    }
}
