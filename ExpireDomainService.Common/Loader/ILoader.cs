using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Loader
{
    public interface ILoader<T> : IDisposable
    {
        bool HasNext();

        T Next();
    }
}
