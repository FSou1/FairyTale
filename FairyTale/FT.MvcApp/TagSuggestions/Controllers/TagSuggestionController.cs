using System.Threading.Tasks;
using System.Web.Mvc;
using FT.MvcApp.Filters;
using FT.MvcApp.TagSuggestions.Models;
using FT.MvcApp.TagSuggestions.Services;

namespace FT.MvcApp.TagSuggestions.Controllers {
    public class TagSuggestionController : Controller {
        public TagSuggestionController(ITagSuggestionService service) {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("tag-suggestion/suggest-story-tag")]
        [Transaction]
        public async Task<ActionResult> SuggestTag(SuggestTagViewModel model) {
            await _service.SuggestTag(model.TaleId, model.SuggestedTagId);

            return Content("success");
        }

        private readonly ITagSuggestionService _service;
    }
}