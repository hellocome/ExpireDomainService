using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Core.Schedule.CheckPoint
{
    public sealed class RunOnServiceStartCheckPoint : RunOnceCheckPoint
    {
        public RunOnServiceStartCheckPoint(String parameter) : base(parameter)
        {

        }

        public RunOnServiceStartCheckPoint()
        { }

        public override void Update() { }

        public override string ToString()
        {
            return string.Format("RunOnServiceStartCheckPoint Runs as soon as service starts");
        }
    }
}
