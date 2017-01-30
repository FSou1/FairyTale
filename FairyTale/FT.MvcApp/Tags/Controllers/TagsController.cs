using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FT.MvcApp.Tags.Models;
using FT.MvcApp.Tags.Services;
using System.Web.UI;

namespace FT.MvcApp.Tags.Controllers {
    public class TagsController : Controller {
        private readonly TagsService _service = new TagsService();
        private const int TalesPerPage = 5;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public ActionResult Single(SingleParams param) {
            var model = _service.BuildSingleViewModel(param.Id, param.CurrentPage, TalesPerPage);
            if (model.Tag == null)
            {
                throw new HttpException(404, "ola");
            }

            return View("Single", model);
        }

        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var model = _service.BuildIndexViewModel();
            return View("Index", model);
        }
    }
}