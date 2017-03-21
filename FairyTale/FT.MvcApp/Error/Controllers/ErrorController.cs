using System.Net;
using System.Web.Mvc;
using FT.MvcApp.Error.Models;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.Error.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Catch 404
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound() {
            Response.StatusCode = 404;

            var model = new ErrorViewModel {
                Title = "404. Страница не найдена"
            };

            return View(model);
        }
    }
}