using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Collection
{
    public sealed class PagedSet<T>  : IEnumerable<T>
    {
        internal SortedSet<T> internalSet;
        private const int DEFAULT_PAGE_SIZE = 10;
        private int mPageSize = 10;

        public PagedSet(int pageSize = DEFAULT_PAGE_SIZE)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("Invalid page size, it must be >= 1, current size is: " + pageSize);
            }

            internalSet = new SortedSet<T>();
            mPageSize = pageSize;
        }

        public PagedSet(IComparer<T> comparer, int pageSize = DEFAULT_PAGE_SIZE)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("Invalid page size, it must be >= 1, current size is: " + pageSize);
            }

            internalSet = new SortedSet<T>(comparer);
            mPageSize = pageSize;
        }

        public int TotalPages
        {
            get
            {
                lock (internalSet)
                {
                    if(internalSet.Count == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return (internalSet.Count / mPageSize + 1);
                    }
                }
            }
        }

        public T[] GetPage(int index)
        {
            lock (internalSet)
            {
                if (index < 0 || index >= internalSet.Count)
                {
                    // Zero size array
                    return new T[0];
                }

                int size = index + mPageSize < internalSet.Count ? mPageSize : internalSet.Count - index;
                T[] array = new T[size];

                internalSet.CopyTo(array, index, size);

                return array;
            }
        }

        #region IEnumerable<T>, IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return internalSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalSet.GetEnumerator();
        }

        #endregion

        #region ICollection

        public int Count
        {
            get
            {
                return internalSet.Count;
            }
        }

        public void Clear()
        {
            internalSet.Clear();
        }

        public bool Contains(T item)
        {
            return internalSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            internalSet.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return internalSet.Remove(item);
        }
        #endregion

        #region ISet
        public bool Add(T item)
        {
            return internalSet.Add(item);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            internalSet.ExceptWith(other);
        }


        public void IntersectWith(IEnumerable<T> other)
        {
            internalSet.IntersectWith(other);
        }


        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return internalSet.IsProperSubsetOf(other);
        }


        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return internalSet.IsProperSupersetOf(other);
        }


        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return internalSet.IsSubsetOf(other);
        }


        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return internalSet.IsSupersetOf(other);
        }


        public bool Overlaps(IEnumerable<T> other)
        {
            return internalSet.Overlaps(other);
        }


        public bool SetEquals(IEnumerable<T> other)
        {
            return internalSet.SetEquals(other);
        }


        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            internalSet.SymmetricExceptWith(other);
        }


        public void UnionWith(IEnumerable<T> other)
        {
            internalSet.UnionWith(other);
        }
        #endregion
    }
}
