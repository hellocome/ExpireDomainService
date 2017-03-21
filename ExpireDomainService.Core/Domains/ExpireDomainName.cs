using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using DomainParser.Library;

namespace ExpireDomainService.Core.Domains
{
    public sealed class ExpireDomainName : DomainParser.Library.DomainName
    {
        public string FullDomainName
        {
            get;
            private set;
        }

        public DateTime ExpireDate
        {
            get;
            private set;
        }

        public bool Expired
        {
            get
            {
                return DateTime.Now > ExpireDate;
            }
        }

        public int ExpiredInDays
        {
            get
            {
                return ExpireDate.Day - DateTime.Now.Day;
            }
        }

        public ExpireDomainName(String fullDomainName, String expireDate) : base(fullDomainName)
        {
            FullDomainName = fullDomainName;
            ExpireDate = DateTime.Parse(expireDate, CultureInfo.GetCultureInfo("en-us"));
        }
    }
}
