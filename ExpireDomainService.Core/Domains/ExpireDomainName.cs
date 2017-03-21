using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using DomainParser.Library;

namespace ExpireDomainService.Core.Domains
{
    public sealed class ExpireDomainName : DomainParser.Library.DomainName, IComparable<ExpireDomainName>
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

        public int CompareTo(ExpireDomainName other)
        {
            if(other == null || string.IsNullOrEmpty(other.SLD))
            {
                return -1;
            }
            else if (string.IsNullOrEmpty(this.SLD))
            {
                return 1;
            }

            int res = this.SLD.Length.CompareTo(other.SLD.Length);

            if(res == 0)
            {
                res = this.SLD.CompareTo(other.SLD);
            }

            return res;
        }
    }
}
