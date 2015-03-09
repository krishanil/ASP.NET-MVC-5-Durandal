using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

using Microsoft.Ajax.Utilities;

namespace WebApplication.Utils.Extentions
{
    public static class ViewDataExtentions
    {
        public static string DataBinding(this ViewDataDictionary viewData)
        {
            var dataBinding = viewData.ToList().FirstOrDefault(d => d.Key == "data_bind");
            Func<KeyValuePair<string, object>, string> binding = bind => string.Format("{0}={1}", bind.Key, bind.Value.ToString().Replace(" ", string.Empty));
            return dataBinding.IfNotNull(binding, string.Empty);
        }
    }
}