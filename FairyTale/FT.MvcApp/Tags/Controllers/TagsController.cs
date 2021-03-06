﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FT.MvcApp.Tags.Models;
using FT.MvcApp.Tags.Services;

namespace FT.MvcApp.Tags.Controllers {
    public class TagsController : Controller {
        private static readonly int TalesPerPage = AppPropertyKeys.TalesPerPage;

        public TagsController(ITagsBuilder builder) {
            _builder = builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("tag/{id}")]
        [OutputCache(Duration = 60, VaryByParam = "*")]
        public async Task<ActionResult> Single(SingleParams param) {
            var model = await _builder.BuildSingleViewModel(param.Id, param.PageIndex, TalesPerPage);

            if (model.Tag == null)
            {
                throw new HttpException(404, "ola");
            }
            if (param.Page != null)
            {
                model.Title += $" - Страница {param.Page}";
                model.CanonicalUrl = Url.Action("Single", "Tags", new { id = param.Id }, "http");
            }

            return View("Single", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("tags")]
        [OutputCache(Duration = 60, VaryByParam = "none")]
        public async Task<ActionResult> Index()
        {
            var model = await _builder.BuildIndexViewModel();

            return View("Index", model);
        }

        private readonly ITagsBuilder _builder;
    }
}