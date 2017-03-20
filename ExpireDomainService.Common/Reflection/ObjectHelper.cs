using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ExpireDomainService.Common.Reflection
{
    public static class ObjectHelper
    {
        public static T Create<T>(string implementationClass) where T : class
        {
            Type tType = Type.GetType(implementationClass);
            Object implObj = Activator.CreateInstance(tType);
            return implObj as T;
        }

        public static T Create<T>(string moduleName, string implementationClass) where T : class
        {
            Assembly module = module = Assembly.LoadFile(moduleName);
            return module.CreateInstance(implementationClass) as T;
        }
    }
}
