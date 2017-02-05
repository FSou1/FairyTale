using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
            var model = await _builder.BuildManageViewModel(id);
            if (model.FairyTale == null)
            {
                throw new HttpException(404, "ola");
            }

            return View("Manage", model);
        }

        [Route("manage-story/format-all")]
        [Transaction]
        public async Task<ActionResult> FormatAll() {
            var notFormattedFairyTales = await _service.GetAllAsync(x => 
                    !x.Text.StartsWith("<p>") || !x.Text.Contains(Environment.NewLine), 0, 100);
            if (notFormattedFairyTales.Count == 0) {
                throw new HttpException(404, "done");
            }

            foreach (var tale in notFormattedFairyTales) {
                tale.Text = Format(tale.Text);
                await _service.UpdateAsync(tale);
            }

            return Content($"Обновлено: {notFormattedFairyTales.Count}");
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

            fairyTale.Text = Format(fairyTale.Text);

            var model = await _builder.BuildManageViewModel(id);
            model.FairyTale = fairyTale;

            return View("Manage", model);
        }

        private string Format(string text) {
            var options = FormatTextOptions.Split
                          | FormatTextOptions.Wrap | FormatTextOptions.Join
                          | FormatTextOptions.Trim | FormatTextOptions.Filter
                          | FormatTextOptions.Replace;

            var formatOptions = new FairyTalesFormatterOptions {
                SplitSeparator = new[] { "<p>", "</p>" },
                WrapStart = "<p>",
                WrapEnd = "</p>",
                JoinSeparator = $"{Environment.NewLine}{Environment.NewLine}",
                TrimChars = new[] { ' ', '\r', '\n' },
                FilterCondition = x => !Regex.IsMatch(x, "[а-яА-Я0-9]"),
                Replace = new Dictionary<string, string>() { { "<br />", "</p><p>"} }
            };

            return StringFormatter.Format(text, options, formatOptions);
        }

        private readonly IFairyTalesBuilder _builder;
        private readonly IFairyTalesService _service;
    }
}