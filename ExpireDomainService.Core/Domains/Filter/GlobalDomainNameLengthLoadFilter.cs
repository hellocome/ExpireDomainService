using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Common.Filter.Filters;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Core.Domains.Filter
{
    public sealed class GlobalDomainNameLengthLoadFilter : DomainNameLengthFilter
    {
        public GlobalDomainNameLengthLoadFilter(String parameter) : base(parameter)
        {

        }
    }
}
