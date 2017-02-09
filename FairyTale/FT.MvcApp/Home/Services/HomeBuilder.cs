﻿using FT.MvcApp.Home.Models;
using FT.Repositories;
using MvcPaging;
using System;
using System.Linq;
using System.Threading.Tasks;
using FT.Components.Utility;
using FT.Entities;

namespace FT.MvcApp.Home.Services {
    public class HomeBuilder : IHomeBuilder {
        public HomeBuilder(IRepository<FairyTale> repository) {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IndexViewModel> BuildIndexViewModel(int page, int perPage) {
            var filter = PredicateBuilder.True<FairyTale>()
                .And(ft => ft.Parent == null);

            var totalCount = await _repository.CountAsync(filter);
            var data = await _repository.GetAllAsync(filter, ft => Guid.NewGuid(), page*perPage, perPage);

            var model = new IndexViewModel() {
                Title = "«ВСказки» - вновь навстречу чуду",
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
            var filter = PredicateBuilder.True<FairyTale>()
                .And(ft => ft.Title.Contains(term));

            var totalCount = await _repository.CountAsync(filter);
            var data = await _repository.GetAllAsync(filter, page*perPage, perPage);

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

    public interface IHomeBuilder {
        Task<IndexViewModel> BuildIndexViewModel(int page, int perPage);

        Task<SearchViewModel> BuildSearchViewModel(string term, int page, int perPage);

        AboutViewModel BuildAboutViewModel();
    }
}