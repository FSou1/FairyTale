using FT.MvcApp.Home.Models;
using FT.Repositories;
using MvcPaging;
using System;
using System.Linq;
using System.Threading.Tasks;
using FT.Entities;
using FT.Repositories.Fake;

namespace FT.MvcApp.Home.Services {
    public class HomeService : IHomeService {
        public HomeService(IRepository<FairyTale> repository) {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IndexViewModel> BuildIndexViewModel(int page, int perPage) {
            var totalCount = await _repository.CountAsync();
            var data = await _repository.GetAllAsync(ft => true, ft => Guid.NewGuid(), page*perPage, perPage);

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
        public async Task<SearchViewModel> BuildSearchViewModel(string term, int page, int perPage) {
            var totalCount = await _repository.CountAsync(ft => ft.Title.Contains(term));
            var data = await _repository.GetAllAsync(ft => ft.Title.Contains(term), page*perPage, perPage);

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

        private readonly IRepository<FairyTale> _repository;
    }

    public interface IHomeService {
        Task<IndexViewModel> BuildIndexViewModel(int page, int perPage);

        Task<SearchViewModel> BuildSearchViewModel(string term, int page, int perPage);

        AboutViewModel BuildAboutViewModel();
    }
}