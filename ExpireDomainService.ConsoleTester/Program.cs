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
            DomainNameProvider manager = new DomainNameProvider();
            manager.SetLoader(ServiceConfiguration.Instance.DomainLoader);
            manager.BuildCache();
        }
    }
}
