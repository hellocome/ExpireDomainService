using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Filter.Filters
{
    public abstract class StringParameterFilter<T> : AbstractFilter<T>
    {
        public IList<String> StringParameters
        {
            get;
            protected set;
        }

        public StringParameterFilter(String parameter)
        {
            LoadParameters(parameter);
        }

        protected virtual void LoadParameters(String parameter)
        {
            List<String> paramList = new List<string>();

            if (!string.IsNullOrEmpty(parameter))
            {
                string[] parameters = parameter.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                paramList.AddRange(parameters);
            }

            StringParameters = paramList.AsReadOnly();
        }
    }
}
