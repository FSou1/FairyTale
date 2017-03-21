using System.Collections.Generic;
using System.Threading.Tasks;
using FT.MvcApp.Home.Models;
using FT.MvcApp.Home.Services;
using System.Web.Mvc;

namespace FT.MvcApp.Home.Controllers
{
    public class HomeController : Controller
    {
        private static readonly int PerPage = AppPropertyKeys.TalesPerPage;

        public HomeController(IHomeBuilder builder) {
            _builder = builder;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="param"></param>
        /// <returns></returns>
        [OutputCache(Duration = 10, VaryByParam = "*")]        
        public async Task<ActionResult> Index(IndexParams param)
        {
            var model = await _builder.BuildIndexViewModel(param.CurrentPage, PerPage);

            if (param.Page != null)
            {
                model.Title += $" - Страница {param.Page}";
                model.CanonicalUrl = Url.Action("Index", "Home", new { }, "http");
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("search")]
        [OutputCache(Duration = 60, VaryByParam = "*")]
        public async Task<ActionResult> Search(SearchParams param)
        {
            if (!param.IsValid) {
                return RedirectToAction("Index");
            }

            var model = await _builder.BuildSearchViewModel(param.Term, param.StartsWith, param.CurrentPage, PerPage);

            model.RouteValues = BuildRouteValues(param.Term, param.StartsWith);

            return View(model);
        }

        private IDictionary<string, object> BuildRouteValues(string term, string startsWith)
        {
            var dict = new Dictionary<string, object>();
            if(!string.IsNullOrEmpty(term))
                dict.Add("term", $"{term}");
            if(!string.IsNullOrEmpty(startsWith))
                dict.Add("startsWith", $"{startsWith}");
            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        [OutputCache(Duration = 60, VaryByParam = "*")]
        public ActionResult About() 
        {
            var model = _builder.BuildAboutViewModel();
            return View(model);
        }

        private readonly IHomeBuilder _builder;
    }
}