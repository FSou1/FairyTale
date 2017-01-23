using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FT.MvcApp.Tags.Models;
using FT.MvcApp.Tags.Services;

namespace FT.MvcApp.Tags.Controllers {
    public class TagsController : Controller {
        private readonly TagsService _service = new TagsService();
        private const int TalesPerPage = 5;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Single(int id) {
            var model = _service.BuildSingleViewModel(id, 0, TalesPerPage);
            if (model.Tag == null)
            {
                throw new HttpException(404, "ola");
            }

            return View("Single", model);
        }
    }
}