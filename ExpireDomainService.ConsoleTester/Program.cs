using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Core;

namespace ExpireDomainService.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            DomainNameManager manager = new DomainNameManager();
            manager.SetLoader(ServiceConfiguration.Instance.DomainLoader);
            manager.BuildCache();
        }
    }
}
