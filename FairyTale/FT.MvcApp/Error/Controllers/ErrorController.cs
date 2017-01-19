using System.Web.Mvc;

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
            return View();
        }

        /// <summary>
        /// Catch 401
        /// </summary>
        /// <returns></returns>
        public ActionResult Unauthorized()
        {
            return View();
        }

        /// <summary>
        /// Catch 404
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return View();
        }
    }
}