using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Schedule
{
    public interface ICheckPoint
    {
        bool Check();

        void Update();

        bool Finished { get; }
    }
}
