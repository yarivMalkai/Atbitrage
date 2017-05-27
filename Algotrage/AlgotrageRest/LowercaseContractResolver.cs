using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgotrageRest
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName.Length > 0)
            {
                return char.ToLower(propertyName[0]) + propertyName.Substring(1);
            }

            return propertyName;
        }
    }
}