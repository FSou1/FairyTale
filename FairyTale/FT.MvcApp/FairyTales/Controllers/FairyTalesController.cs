using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FT.Components.Utility;
using FT.Entities;
using FT.MvcApp.FairyTales.Models;
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

        [Route("manage-story/update")]
        [ValidateInput(false)]
        [Transaction]
        public async Task<ActionResult> Update(SingleViewModel model) {
            var fairyTale = await _service.GetAsync(model.FairyTale.Id);
            if (fairyTale == null) {
                throw new HttpException(404, nameof(fairyTale));
            }

            fairyTale.Text = model.FairyTale.Text;
            await _service.UpdateAsync(fairyTale);

            return RedirectToAction("Manage", new { model.FairyTale.Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("manage-story/format-text/{id}")]
        public async Task<ActionResult> FormatText(int id) {
            var fairyTale = await _service.GetAsync(id);
            if (fairyTale == null) {
                throw new HttpException(404, "ola");
            }

            var options = FormatTextOptions.Split
                          | FormatTextOptions.Wrap | FormatTextOptions.Join
                          | FormatTextOptions.Trim | FormatTextOptions.Filter;

            var formatOptions = new FairyTalesFormatterOptions {
                SplitSeparator = new[] {"<p>", "</p>"},
                WrapStart = "<p>",
                WrapEnd = "</p>",
                JoinSeparator = $"{Environment.NewLine}{Environment.NewLine}",
                TrimChars = new[] {' '},
                FilterCondition = x => string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x)
            };

            fairyTale.Text = StringFormatter.Format(fairyTale.Text, options, formatOptions);

            var model = new SingleViewModel {
                FairyTale = fairyTale
            };

            return View("Manage", model);
        }

        private readonly IFairyTalesBuilder _builder;
        private readonly IFairyTalesService _service;
    }
}