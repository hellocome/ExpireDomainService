using System;
using System.Text.RegularExpressions;
using ExpireDomainService.Common.Filter.Filters;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Core.Domains.Filter.CacheFilters
{
    public sealed class RegexDomainNameCacheFilter : RegexFilter<ExpireDomainName>
    {
        public RegexDomainNameCacheFilter(String parameter) : base(parameter)
        {
            Logger.Instance.Info("RegexFullDomainCacheFilter.Parameters = {0}", parameter);

            foreach (string dName in this.StringParameters)
            {
                Logger.Instance.Info("RegexFullDomainCacheFilter domain will be load = {0}", dName);
            }
        }

        public override bool Decide(ExpireDomainName item)
        {
            foreach (string dName in this.StringParameters)
            {
                if (Match(item.SLD, dName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
