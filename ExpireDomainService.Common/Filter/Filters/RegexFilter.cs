using System;
using System.Text.RegularExpressions;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Common.Filter.Filters
{
    public abstract class RegexFilter<T> : StringParameterFilter<T>
    {
        public RegexFilter(String parameter) : base(parameter)
        {
            Logger.Instance.Info("RegexCacheFilter.Parameters = {0}", parameter);

            foreach (string dName in this.StringParameters)
            {
                Logger.Instance.Info("RegexCacheFilter domain will be load = {0}", dName);
            }
        }

        protected bool Match(String input, String pattern)
        {
            try
            {
                return Regex.IsMatch(input, pattern);
            }
            catch (Exception ex)
            {
                Logger.Instance.Warning("Fail to match", ex);
                return false;
            }
        }
    }
}
