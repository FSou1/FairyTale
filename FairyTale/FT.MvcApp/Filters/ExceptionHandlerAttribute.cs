using System.Web.Mvc;
using log4net;

namespace FT.MvcApp.Filters {
    public class ExceptionHandlerAttribute : HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext) {
            LogManager.GetLogger(typeof(ExceptionHandlerAttribute)).Error(filterContext);

            base.OnException(filterContext);
        }
    }
}