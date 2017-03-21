using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FT.MvcApp.AlphaIndex.Models;
using FT.MvcApp.AlphaIndex.Services;

namespace FT.MvcApp.AlphaIndex.Controllers
{
    public class AlphaIndexController : Controller
    {
        public AlphaIndexController(IAlphaIndexBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "*")]
        public async Task<ActionResult> Index(IndexParams viewModel)
        {
            var model = await _builder.BuildIndexModel();

            var letters = model.TalesLetters.Concat(model.TalesLetters).Distinct().OrderBy(l => l)
                .ToList();

            return PartialView("Letters", letters);
        }

        private readonly IAlphaIndexBuilder _builder;
    }
}