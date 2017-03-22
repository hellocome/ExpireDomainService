using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Patterns
{
    public abstract class AbstractObserable<T> : IObservable<T>
    {
        // Do we need concurrent?
        protected List<IObserver<T>> observers = new List<IObserver<T>>();

        public virtual IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            public List<IObserver<T>> innerObservers = null;
            public IObserver<T> innerObserver = null;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                innerObservers = observers;
                innerObserver = observer;
            }

            public void Dispose()
            {
                if(innerObservers != null && innerObservers.Contains(innerObserver))
                {
                    innerObservers.Remove(innerObserver);
                }
            }
        }
    }
}
