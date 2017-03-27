using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSpace(this string input)
        {
            // new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray())
            return Regex.Replace(input, @"\s", "");
        }
    }
}
