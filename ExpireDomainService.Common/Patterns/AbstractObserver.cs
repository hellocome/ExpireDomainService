using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Patterns
{
    public abstract class AbstractObserver<T> : IObserver<T>
    {
        private IDisposable unsubscribler = null;

        public void Subscribe(IObservable<T> observable)
        {
            if (observable != null)
                unsubscribler = observable.Subscribe(this);
        }


        public virtual void OnCompleted()
        {
            Unsubscribe();
        }

        public virtual void Unsubscribe()
        {
            if (unsubscribler != null)
            {
                unsubscribler.Dispose();
            }
        }

        public abstract void OnError(Exception error);

        public abstract void OnNext(T value);
    }
}
