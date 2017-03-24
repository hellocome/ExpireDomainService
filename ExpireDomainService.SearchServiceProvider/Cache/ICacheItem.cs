using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.SearchServiceProvider.Cache
{
    public interface ICacheItem<TKey, TValue>
    {
        TKey CacheItemKey
        {
            get;
        }

        TValue CacheItemValue
        {
            get;
        }
    }
}
