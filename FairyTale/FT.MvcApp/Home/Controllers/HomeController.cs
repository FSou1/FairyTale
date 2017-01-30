﻿using System.Threading.Tasks;
using FT.MvcApp.Home.Models;
using FT.MvcApp.Home.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FT.MvcApp.Home.Controllers
{
    public class HomeController : Controller
    {
        private const int PerPage = 5;

        public HomeController(IHomeService service) {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="param"></param>
        /// <returns></returns>
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Server)]
        public async Task<ActionResult> Index(IndexParams param)
        {
            var model = await _service.BuildIndexViewModel(param.CurrentPage, PerPage);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("search")]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public async Task<ActionResult> Search(SearchParams param)
        {
            if (string.IsNullOrEmpty(param.Term)) {
                return RedirectToAction("Index");
            }

            var model = await _service.BuildSearchViewModel(param.Term, param.CurrentPage, PerPage);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        public ActionResult About() 
        {
            var model = _service.BuildAboutViewModel();
            return View(model);
        }

        private readonly IHomeService _service;
    }
}