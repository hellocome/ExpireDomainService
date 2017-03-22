using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpireDomainService.Common.WCF
{
    public interface IWcfService
    {
        void OpenService();
        void CloseService();
        void Init();
        bool Term();
        bool Initialised { get; }
        string StartupStatus { get; }
    }
}
