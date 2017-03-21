using FT.MvcApp.Home.Models;
using FT.Repositories;
using MvcPaging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FT.Components.Utility;
using FT.Entities;
using FT.Entities.Contract;

namespace FT.MvcApp.Home.Services {
    public class HomeBuilder : IHomeBuilder {
        public HomeBuilder(
            IRepository<FairyTale> taleRepository,
            IRepository<Tag> tagRepository
        )
        {
            _taleRepository = taleRepository;
            _tagRepository = tagRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IndexViewModel> BuildIndexViewModel(int page, int perPage) {
            var filter = PredicateBuilder.True<FairyTale>()
                .And(ft => ft.Parent == null);

            var totalCount = await _taleRepository.CountAsync(filter);
            var data = await _taleRepository.GetAllAsync(filter, ft => ft.Summary.Likes, false, page*perPage, perPage);

            var model = new IndexViewModel() {
                Title = "«ВСказки» - вновь навстречу чуду",
                Description = "Vskazki.ru - сказки народов мира, библиотека авторских сказок для детей и взрослых.",
                RandomFairyTales = data.ToPagedList(page, perPage, totalCount)
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <param name="startsWith"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<SearchViewModel> BuildSearchViewModel(string term, string startsWith, int page, int perPage) {
            var taleFilter = BuildFilter<FairyTale>(term, startsWith);
            var tagFilter = BuildFilter<Tag>(term, startsWith);
            
            var tales = await _taleRepository.GetAllAsync(taleFilter.Filter, x => x.Title, true, page*perPage, perPage);
            var tags = await _tagRepository.GetAllAsync(tagFilter.Filter, x => x.Title, true);
            var talesTotalCount = await _taleRepository.CountAsync(taleFilter.Filter);

            var model = new SearchViewModel()
            {
                Title = $"Результаты поиска по запросу {term}",
                Description = $"Поиск сказок и тегов в библиотеке по запросу {term}",
                Query = taleFilter.Query,
                FoundFairyTales = tales.ToPagedList(page, perPage, talesTotalCount),
                FoundTags = tags
            };

            return model;
        }

        private FilterQuery<T> BuildFilter<T>(string term, string startsWith) where T : IEntity
        {
            var filter = PredicateBuilder.True<T>();
            string query = null;

            if (!string.IsNullOrEmpty(term) 
                && !string.IsNullOrWhiteSpace(term))
            {
                filter = filter.And(ft => ft.Title.Contains(term));
                query += $"содержит {term}";
            }

            if (!string.IsNullOrEmpty(startsWith) 
                && !string.IsNullOrWhiteSpace(startsWith))
            {
                filter = filter.And(ft => ft.Title.StartsWith(startsWith));
                query += $"начинается на {startsWith}";
            }

            return new FilterQuery<T>()
            {
                Query = query,
                Filter = filter
            };
        }

        internal class FilterQuery<T>
        {
            internal string Query { get; set; }
            internal Expression<Func<T, bool>> Filter { get; set; }
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

        private readonly IRepository<FairyTale> _taleRepository;
        private readonly IRepository<Tag> _tagRepository;
    }

    public interface IHomeBuilder {
        Task<IndexViewModel> BuildIndexViewModel(int page, int perPage);

        Task<SearchViewModel> BuildSearchViewModel(string term, string startsWith, int page, int perPage);

        AboutViewModel BuildAboutViewModel();
    }
}