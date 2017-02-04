using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FT.MvcApp.Tags.Models;
using FT.MvcApp.Tags.Services;
using System.Web.UI;

namespace FT.MvcApp.Tags.Controllers {
    public class TagsController : Controller {
        private const int TalesPerPage = 5;

        public TagsController(ITagsBuilder builder) {
            _builder = builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        [Route("tag/{id}")]
        public async Task<ActionResult> Single(SingleParams param) {
            var model = await _builder.BuildSingleViewModel(param.Id, param.CurrentPage, TalesPerPage);
            if (model.Tag == null)
            {
                throw new HttpException(404, "ola");
            }

            return View("Single", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        [Route("tags")]
        public async Task<ActionResult> Index()
        {
            var model = await _builder.BuildIndexViewModel();

            return View("Index", model);
        }

        private readonly ITagsBuilder _builder;
    }
}