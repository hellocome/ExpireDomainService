using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHLParcelShopFinder.Schedule
{
    public interface ICheckPoint
    {
        bool Check();

        void Update();
    }
}
