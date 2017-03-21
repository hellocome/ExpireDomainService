using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Common.Filter.Filters;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Core.Domains.Filter
{
    public sealed class GlobalDomainLoadFilter : StringParameterFilter<ExpireDomainName>
    {
        public GlobalDomainLoadFilter(String parameter) : base(parameter)
        {
            Logger.Instance.Info("GlobalDomainLoadFilter.Parameters = {0}", parameter);

            foreach(string dName in this.StringParameters)
            {
                Logger.Instance.Info("GlobalDomainLoadFilter domain will be load = {0}", dName);
            }
        }

        public override bool Decide(ExpireDomainName item)
        {
            if (this.StringParameters.Contains(item.TLD))
            {
                return true;
            }

            return false;
        }
    }
}
