using FT.MvcApp.Home.Models;
using FT.MvcApp.Home.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FT.MvcApp.Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _service = new HomeService();
        private const int PerPage = 5;

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="param"></param>
        /// <returns></returns>
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Server)]
        public ActionResult Index(IndexParams param)
        {
            var model = _service.BuildIndexViewModel(param.CurrentPage, PerPage);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("search")]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public ActionResult Search(SearchParams param)
        {
            if (string.IsNullOrEmpty(param.Term)) {
                return RedirectToAction("Index");
            }

            var model = _service.BuildSearchViewModel(param.Term, param.CurrentPage, PerPage);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        public ActionResult About() 
        {
            var model = _service.BuildAboutViewModel();
            return View(model);
        }
    }
}