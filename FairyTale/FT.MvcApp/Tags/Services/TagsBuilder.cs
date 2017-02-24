using FT.MvcApp.Tags.Models;
using FT.Repositories;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FT.Components.Utility;
using FT.Entities;

namespace FT.MvcApp.Tags.Services
{
    public class TagsBuilder : ITagsBuilder {
        public TagsBuilder(
            IRepository<Tag> repository,
            IRepository<FairyTale> ftRepository
        ) {
            _repository = repository;
            _ftRepository = ftRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<SingleViewModel> BuildSingleViewModel(int id, int page, int perPage)
        {
            var tag = await _repository.GetAsync(id);

            var filter = PredicateBuilder.True<FairyTale>()
                .And(ft => ft.Parent == null)
                .And(ft => ft.Tags.Contains(tag));
            
            var ftData = await _ftRepository.GetAllAsync(filter, page*perPage, perPage);
            var ftTotalCount = await _ftRepository.CountAsync(filter);

            var model = new SingleViewModel
            {
                Title = "Просмотр",
                Description = tag.Description,
                Tag = tag,
                FairyTales = ftData.ToPagedList(page, perPage, ftTotalCount)
            };

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IndexViewModel> BuildIndexViewModel() {
            var list = await _repository.GetAllAsync(t => t.FairyTalesCount, false);

            var dict = list
                .GroupBy(x => x.Type)
                .ToDictionary(k => k.Key, v=> v.ToList());

            var model = new IndexViewModel()
            {
                Title = "Список тегов",
                Description = "Удобный сгруппированный каталог авторских и народных сказок",
                Tags = dict
            };

            return model;
        }

        private readonly IRepository<Tag> _repository;
        private readonly IRepository<FairyTale> _ftRepository;
    }

    public interface ITagsBuilder {
        Task<SingleViewModel> BuildSingleViewModel(int id, int page, int perPage);

        Task<IndexViewModel> BuildIndexViewModel();
    }
}