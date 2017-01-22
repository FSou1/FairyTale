using FT.MvcApp.Home.Models;
using FT.Repositories;
using MvcPaging;
using System;
using System.Linq;

namespace FT.MvcApp.Home.Services {
    public class HomeService {
        private readonly FairyTaleRepository _repository = new FairyTaleRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IndexViewModel BuildIndexViewModel(int page, int perPage) {
            var totalCount = _repository.Count();
            var data = _repository.GetAll(ft => Guid.NewGuid(), page, perPage);

            var model = new IndexViewModel() {
                Title = "Главная страница",
                RandomFairyTales = data.ToPagedList(page, perPage, totalCount)
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public SearchViewModel BuildSearchViewModel(string term, int page, int perPage)
        {
            var totalCount = _repository.Count(term);
            var data = _repository.GetAll(term, page * perPage, perPage);

            var model = new SearchViewModel()
            {
                Title = "Поиск",
                Term = term,
                FoundFairyTales = data.ToPagedList(page, perPage, totalCount)
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