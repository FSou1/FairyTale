using System.Web.Mvc;
using FT.Components.Serializer;
using FT.Components.Serializer.Json;
using log4net;

namespace FT.MvcApp.Filters {
    public class ExceptionHandlerAttribute : HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext) {
            var serializer = DependencyResolver.Current.GetService<ISerializer>() 
                ?? new NewtonsoftJsonSerializer();

            var exception = filterContext.Exception;
            var json = serializer.Serialize(exception);

            LogManager.GetLogger(typeof(ExceptionHandlerAttribute)).Error(json);

            base.OnException(filterContext);
        }
    }
}