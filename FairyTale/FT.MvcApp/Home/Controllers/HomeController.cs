using FT.MvcApp.Home.Services;
using System.Web.Mvc;

namespace FT.MvcApp.Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService service = new HomeService();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = service.BuildIndexViewModel();
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        public ActionResult About() 
        {
            var model = service.BuildAboutViewModel();
            return View(model);
        }
    }
}