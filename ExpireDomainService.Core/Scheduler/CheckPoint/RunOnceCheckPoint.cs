using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Core.Schedule.CheckPoint
{
    public abstract class RunOnceCheckPoint : ICheckPoint
    {
        private object locker = new object();

        public RunOnceCheckPoint()
        {
            finished = false;
        }

        private volatile bool finished;
        public bool Finished
        {
            get
            {
                return finished;
            }
        }

        public bool Check()
        {
            lock (locker)
            {
                if (!finished)
                {
                    finished = true;
                    return true;
                }

                return false;
            }
        }

        public abstract void Update();
    }
}
