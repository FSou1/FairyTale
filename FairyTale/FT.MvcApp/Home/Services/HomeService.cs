using FT.MvcApp.Home.Models;
using FT.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.MvcApp.Home.Services {
    public class HomeService {
        private readonly FairyTaleRepository _repository = new FairyTaleRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IndexViewModel BuildIndexViewModel() {
            var model = new IndexViewModel() {
                Title = "Главная страница",
                RandomFairyTales = _repository.GetAll()
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public SearchViewModel BuildSearchViewModel(string term, int page, int perPage)
        {
            var model = new SearchViewModel(perPage)
            {
                Title = "Поиск",
                Term = term,
                FoundFairyTales = _repository.GetAll(term, page * perPage, perPage),
                TotalCount = _repository.Count(term),
                CurrentPage = page
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AboutViewModel BuildAboutViewModel() {
            var model = new AboutViewModel() {
                Title = "О сайте"
            };

            return model;
        }
    }
}