using FT.MvcApp.Tags.Models;
using FT.Repositories;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.MvcApp.Tags.Services
{
    public class TagsService
    {
        private readonly TagRepository repository = new TagRepository();
        private readonly FairyTaleRepository ftRepository = new FairyTaleRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SingleViewModel BuildSingleViewModel(int id, int page, int perPage)
        {
            var tag = repository.Get(id);
            var ftData = ftRepository.GetAll(tag, page * perPage, perPage);
            var fairyTalesTotalCount = ftRepository.Count(tag);

            var model = new SingleViewModel
            {
                Title = "Просмотр",
                Tag = tag,
                FairyTales = ftData.ToPagedList(page, perPage, fairyTalesTotalCount)
            };

            return model;
        }
    }
}