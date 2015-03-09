using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace WebApplication.Utils.Extentions
{
    public static class ViewDataExtentions
    {
        public static string AdditionalData(this ViewDataDictionary viewData)
        {
            var builder = new StringBuilder();
            viewData.ToList().ForEach(d => builder.Append(string.Format("{0}={1}", d.Key == "data_bind" ? "data-bind" : d.Key, d.Value.ToString().Replace(" ", string.Empty))));
            return builder.ToString();
        }
    }
}