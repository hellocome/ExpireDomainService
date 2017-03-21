using System;
using ExpireDomainService.Common.Filter.Filters;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Core.Domains.Filter
{
    public class DomainNameLengthFilter : StringParameterFilter<ExpireDomainName>
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

        public DomainNameLengthFilter(String parameter) : base(parameter)
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
                    MinLength = MaxLength;
                }
                else if (this.StringParameters.Count == 2)
                {
                    MinLength = int.Parse(this.StringParameters[0]);
                    MaxLength = int.Parse(this.StringParameters[1]);
                }

                if (MinLength >= 0 && MaxLength >= MinLength)
                {
                    ValidMaxMinLength = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Fail to load min and max length for " + this.GetType().Name, ex);
            }

            if (!ValidMaxMinLength)
            {
                Logger.Instance.Error("Invalid Max and Min Length: MinLength = {0}, MaxLength = {1}, default value will be used", MinLength, MaxLength);
                MinLength = 0;
                MaxLength = int.MaxValue;
            }

            Logger.Instance.Info("{0} MinLength = {1}, MaxLength = {2}", this.GetType().Name, MinLength, MaxLength);
        }

        public override bool Decide(ExpireDomainName item)
        {
            if (item.SLD.Length >= MinLength && item.SLD.Length <= MaxLength)
            {
                return true;
            }

            return false;
        }
    }
}
