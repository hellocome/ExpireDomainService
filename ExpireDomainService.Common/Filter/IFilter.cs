using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Filter
{
    public interface IFilter<T>
    {
        bool Decide(T item);
    }
}
