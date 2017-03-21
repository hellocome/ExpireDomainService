using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Filter
{
    public abstract class AbstractFilter<T> : IFilter<T>
    {
        public string UID
        {
            get;
            set;
        }

        public abstract bool Decide(T item);
    }
}
