using System.Web.Mvc;
using FT.MvcApp.Error.Models;

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

        /// <summary>
        /// Catch 500
        /// </summary>
        /// <returns></returns>
        public ActionResult Internal()
        {
            Response.StatusCode = 500;

            var model = new ErrorViewModel
            {
                Title = "500. К сожалению мы не смогли обработать ваш запрос :("
            };

            return View(model);
        }
    }
}