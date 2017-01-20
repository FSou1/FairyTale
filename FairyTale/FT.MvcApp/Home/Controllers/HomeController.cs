using FT.MvcApp.Home.Models;
using FT.MvcApp.Home.Services;
using System.Web;
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
        /// <param name="term"></param>
        /// <returns></returns>
        [Route("search")]
        public ActionResult Search(SearchParams param)
        {
            if (string.IsNullOrEmpty(param.Term))
                throw new HttpException(400, "Term is null or empty");

            var model = service.BuildSearchViewModel(param.Term);
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