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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("manage-story/{id}")]
        public async Task<ActionResult> Manage(int id)
        {
            var model = await _service.BuildSingleViewModel(id);
            if (model.FairyTale == null)
            {
                throw new HttpException(404, "ola");
            }

            return View("Manage", model);
        }

        private readonly IFairyTalesService _service;
    }
}