using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FT.MvcApp.FairyTales.Services;
using FT.MvcApp.Filters;

namespace FT.MvcApp.FairyTales.Controllers
{
    public class FairyTalesController : Controller
    {
        public FairyTalesController(IFairyTalesBuilder builder, IFairyTalesService service) {
            _builder = builder;
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("story/{id}")]
        public async Task<ActionResult> Single(int id)
        {
            var model = await _builder.BuildSingleViewModel(id);
            if (model.FairyTale == null) {
                throw new HttpException(404, "ola");
            }

            return View("Single", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("manage-story/{id}")]
        public async Task<ActionResult> Manage(int id)
        {
            var model = await _builder.BuildSingleViewModel(id);
            if (model.FairyTale == null)
            {
                throw new HttpException(404, "ola");
            }

            return View("Manage", model);
        }

        [Route("manage-story/add-new-lines/{id}")]
        [Transaction]
        public async Task<ActionResult> AddNewLines(int id) {
            var fairyTale = await _service.GetAsync(id);
            if (fairyTale == null) {
                return HttpNotFound();
            }

            await _service.UpdateTextAsync(fairyTale, UpdateTextOptions.AddNewLines);

            return RedirectToAction("Manage", new {id});
        }

        private readonly IFairyTalesBuilder _builder;
        private readonly IFairyTalesService _service;
    }
}