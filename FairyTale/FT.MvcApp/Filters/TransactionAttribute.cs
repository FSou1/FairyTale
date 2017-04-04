using System.Web.Mvc;
using FT.Repositories;

namespace FT.MvcApp.Filters {
    public class TransactionAttribute : ActionFilterAttribute {
        private IUnitOfWork _unitOfWork;

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var modelValid = filterContext.Controller.ViewData.ModelState.IsValid;
            var error = filterContext.HttpContext.Error;

            if (!modelValid || error != null) {
                return;
            }

            _unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
            _unitOfWork.BeginTransaction();
            
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var modelValid = filterContext.Controller.ViewData.ModelState.IsValid;
            var error = filterContext.HttpContext.Error;

            if (modelValid && error == null)
            {
                _unitOfWork.Commit();
            }
            else
            {
                _unitOfWork.Rollback();
            }

            base.OnResultExecuted(filterContext);
        }
    }
}