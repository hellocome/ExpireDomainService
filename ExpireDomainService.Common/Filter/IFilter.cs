using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Filter
{
    public interface IFilter<T>
    {
        string UID
        {
            get;
        }

        bool Decide(T item);
    }
}
