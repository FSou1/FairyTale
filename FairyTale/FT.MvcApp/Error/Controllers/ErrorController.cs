using System.Web.Mvc;
using FT.MvcApp.Error.Models;
using FT.MvcApp.Shared.Models;

namespace FT.MvcApp.Error.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Catch all
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new ErrorViewModel {
                Title = "500. Что-то пошло явно не так"
            };

            return View(model);
        }

        /// <summary>
        /// Catch 401
        /// </summary>
        /// <returns></returns>
        public ActionResult Unauthorized()
        {
            var model = new ErrorViewModel {
                Title = "401. Доступ запрещён"
            };

            return View(model);
        }

        /// <summary>
        /// Catch 404
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound() {
            var model = new ErrorViewModel {
                Title = "404. Страница не найдена"
            };

            return RedirectToAction("Index", "Home");
        }
    }
}