using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Core.Data
{
    public interface IFilterable 
    {
        bool Decide(IFilterable filterable);
    }
}
