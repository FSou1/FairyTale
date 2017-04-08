using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FT.Components.Interaction;
using FT.MvcApp.FairyTales.Models;
using FT.MvcApp.FairyTales.Services;
using FT.MvcApp.Filters;

namespace FT.MvcApp.FairyTales.Controllers
{
    public class FairyTalesController : Controller
    {
        public FairyTalesController(
            IFairyTalesBuilder builder, 
            IFairyTalesService service,
            IEventBroker eventBroker
        ) {
            _builder = builder;
            _service = service;
            _eventBroker = eventBroker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("story/{id}")]
        [OutputCache(Duration = 60, VaryByParam = "id")]
        public async Task<ActionResult> Single(int id)
        {
            var model = await _builder.BuildSingleViewModel(id);
            if (model.FairyTale == null) {
                throw new HttpException(404, $"Сказка с id {id} не найдена");
            }

            await _eventBroker.TaleViewed(id);

            return View("Single", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("story/increase-views")]
        [Transaction]
        public async Task<ActionResult> IncreaseViews(IncreaseViewsModel model) {
            if (AppPropertyKeys.BaseAccessToken != model.Token) {
                throw new HttpException(401, "Доступ запрещён");
            }

            var fairyTale = await _service.GetAsync(model.Id);
            if (fairyTale == null) {
                throw new HttpException(404, $"Сказка с id {model.Id} не найдена");
            }

            await _service.IncreaseViews(fairyTale);

            return Content("success");
        }

        public class IncreaseViewsModel {
            public int Id { get; set; }
            public string Token { get; set; }
        }

        private readonly IFairyTalesBuilder _builder;
        private readonly IFairyTalesService _service;
        private readonly IEventBroker _eventBroker;
    }
}