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
        public async Task<ActionResult> Single(long id)
        {
            var model = await _service.BuildSingleViewModel(id);
            if (model.FairyTale == null) {
                throw new HttpException(404, "ola");
            }

            return View("Single", model);
        }

        private readonly IFairyTalesService _service;
    }
}