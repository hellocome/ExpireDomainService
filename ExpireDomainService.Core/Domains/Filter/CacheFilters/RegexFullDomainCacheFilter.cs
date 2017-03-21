using System;
using System.Text.RegularExpressions;
using ExpireDomainService.Common.Filter.Filters;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Core.Domains.Filter.CacheFilters
{
    public sealed class RegexFullDomainCacheFilter : RegexFilter<ExpireDomainName>
    {
        public RegexFullDomainCacheFilter(String parameter) : base(parameter)
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
                if (Match(item.FullDomainName, dName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
