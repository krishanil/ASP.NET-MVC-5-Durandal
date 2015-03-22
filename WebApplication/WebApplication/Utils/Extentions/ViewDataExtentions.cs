using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Utils.Extentions
{
    public static class ViewDataExtentions
    {
        public static string DataBinding(this ViewDataDictionary viewData)
        {
            var valueBinding = ValueBinding(viewData.ModelMetadata.ModelType);
            var dataBinding = string.Format("{0}: {1}", valueBinding, viewData.ModelMetadata.PropertyName);
            var additionalBindings = viewData.ToList().FirstOrDefault(d => d.Key == "data_bind");
            return additionalBindings.Value != null ? string.Format("{0}, {1}", dataBinding, additionalBindings.Value) : dataBinding;
        }

        private static string ValueBinding(Type modelType)
        {
            switch (Type.GetTypeCode(modelType))
            {
                case TypeCode.Boolean: return "checked";
                default: return "value";
            }
        }
    }
}