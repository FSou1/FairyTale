using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FT.MvcApp.FairyTales.Services;

namespace FT.MvcApp.FairyTales.Controllers
{
    public class FairyTalesController : Controller
    {
        public FairyTalesController(IFairyTalesService service) {
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
            var model = await _service.BuildSingleViewModel(id);
            if (model.FairyTale == null) {
                throw new HttpException(404, "ola");
            }

            return View("Single", model);
        }

        [Route("dirty")]
        public async Task<ActionResult> Dirty() {
            var model = await _service.BuildDirtyViewModel();
            if (model.FairyTale == null) {
                throw new HttpException(404, "ola");
            }
            
            // cleanup

            return View("Dirty", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("clean/{id}")]
        public async Task<ActionResult> Clean(int id) {
            var model = await _service.BuildSingleViewModel(id);
            if (model.FairyTale == null) {
                throw new HttpException(404, "ola");
            }

            // cleanup

            await _service.UpdateAsync(model.FairyTale);

            return RedirectToAction("Dirty");
        }

        private readonly IFairyTalesService _service;
    }
}