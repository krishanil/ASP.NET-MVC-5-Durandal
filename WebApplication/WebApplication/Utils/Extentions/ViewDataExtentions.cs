using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Utils.Extentions
{
    public static class ViewDataExtentions
    {
        public static string DataBinding(this ViewDataDictionary viewData)
        {
            var dataBinding = viewData.ToList().FirstOrDefault(d => d.Key == "data_bind");
            return dataBinding.Value != null ? dataBinding.Value.ToString() : string.Empty;
        }
    }
}