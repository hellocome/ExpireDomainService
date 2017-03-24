using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTest.Net.Collections;
using ExpireDomainService.SearchServiceProvider.Cache;

namespace ExpireDomainService.SearchServiceProvider.Cache
{
    public sealed class SortedCacheContainer<TKey, TValue> where TKey : class
    {
        internal LurchTable<TKey, ICacheItem<TKey, TValue>> internalCacheContainer;

        public SortedCacheContainer(int MaxSize)
        {
            internalCacheContainer = new LurchTable<TKey, ICacheItem<TKey, TValue>>(MaxSize);
        }

        struct AddUpdateValue<K, V> : ICreateOrUpdateValue<K, V>, IRemoveValue<K, V> 
            where V : class 
            where K : class
        {
            public V OldValue;
            public V Value;
            public bool CreateValue(K key, out V value)
            {
                OldValue = null;
                value = Value;
                return Value != null;
            }
            public bool UpdateValue(K key, ref V value)
            {
                OldValue = value;
                value = Value;
                return Value != null;
            }
            public bool RemoveValue(K key, V value)
            {
                OldValue = value;
                return value == Value;
            }
        }

        public void Clear()
        {
            internalCacheContainer.Clear();
        }

        public void Add(ICacheItem<TKey, TValue> item)
        {
            if (item != null)
            {
                AddUpdateValue<TKey, ICacheItem<TKey, TValue>> addUpdateValue = new AddUpdateValue<TKey, ICacheItem<TKey, TValue>>();
                internalCacheContainer.AddOrUpdate(item.CacheItemKey, ref addUpdateValue);
            }
        }

        public bool TryGetValue(TKey key, out ICacheItem<TKey, TValue> item)
        {
            item = null;

            if (key != null)
            {
                internalCacheContainer.TryGetValue(key, out item);
            }

            return item != null;
        }
    }
}
