using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FT.Repositories;
using Remotion.Linq.Clauses.ResultOperators;

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

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            var modelValid = filterContext.Controller.ViewData.ModelState.IsValid;
            var error = filterContext.HttpContext.Error;

            if (modelValid && error == null) {
                _unitOfWork.Commit();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}