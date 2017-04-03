using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FT.MvcApp.FairyTales.Services;
using FT.MvcApp.Filters;

namespace FT.MvcApp.FairyTales.Controllers
{
    public class FairyTalesController : Controller
    {
        public FairyTalesController(
            IFairyTalesBuilder builder, 
            IFairyTalesService service
        ) {
            _builder = builder;
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction]
        [Route("story/{id}")]
        [OutputCache(Duration = 60, VaryByParam = "id")]
        public async Task<ActionResult> Single(int id)
        {
            var model = await _builder.BuildSingleViewModel(id);
            if (model.FairyTale == null) {
                throw new HttpException(404, "ola");
            }

            await _service.IncreaseViews(model.FairyTale);

            return View("Single", model);
        }

        private readonly IFairyTalesBuilder _builder;
        private readonly IFairyTalesService _service;
    }
}