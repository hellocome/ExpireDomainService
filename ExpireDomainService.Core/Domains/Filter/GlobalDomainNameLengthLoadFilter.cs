using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpireDomainService.Common.Filter.Filters;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Core.Domains.Filter
{
    public sealed class GlobalDomainNameLengthLoadFilter : StringParameterFilter<ExpireDomainName>
    {
        public int MinLength
        {
            get;
            private set;
        }

        public int MaxLength
        {
            get;
            private set;
        }

        public bool ValidMaxMinLength
        {
            get;
            private set;
        }

        public GlobalDomainNameLengthLoadFilter(String parameter) : base(parameter)
        {
            LoadMaxMinLength();
        }

        private void LoadMaxMinLength()
        {
            try
            {
                MinLength = 0;
                MaxLength = int.MaxValue;
                ValidMaxMinLength = false;

                if (this.StringParameters.Count == 1)
                {
                    MaxLength = int.Parse(this.StringParameters[0]);
                }
                else if (this.StringParameters.Count == 2)
                {
                    MinLength = int.Parse(this.StringParameters[0]);
                    MaxLength = int.Parse(this.StringParameters[1]);
                }

                if(MinLength >= 0 && MaxLength >= MinLength)
                {
                    ValidMaxMinLength = true;
                }
            }
            catch(Exception ex)
            {
                MinLength = 0;
                MaxLength = int.MaxValue;

                Logger.Instance.Error("Fail to load min and max length for GlobalDomainNameLengthLoadFilter", ex);
            }

            Logger.Instance.Info("GlobalDomainNameLengthLoadFilter.MinLength = {0}", MinLength);
            Logger.Instance.Info("GlobalDomainNameLengthLoadFilter.MaxLength = {0}", MaxLength);
        }

        public override bool Decide(ExpireDomainName item)
        {
            if (this.StringParameters.Contains(item.Domain))
            {
                return true;
            }

            return false;
        }
    }
}
