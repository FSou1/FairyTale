﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT.Components.Utility;
using FT.Entities;
using FT.MvcApp.FairyTales.Models;
using FT.Repositories;

namespace FT.MvcApp.FairyTales.Services
{
    public class FairyTalesBuilder : IFairyTalesBuilder {
        public FairyTalesBuilder(
            IFairyTaleRepository repository,
            IRepository<SuggestedTag> suggestedTagRepository
        ) {
            _repository = repository;
            _suggestedTagRepository = suggestedTagRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SingleViewModel> BuildSingleViewModel(int id) {
            var model = new SingleViewModel();

            var fairyTale = await _repository.GetAsync(id);
            if (fairyTale == null)
            {
                return model;
            }

            model.FairyTale = fairyTale;
            model.Title = fairyTale.Title;
            model.Description = fairyTale.Description;            
            model.Related = await _repository.Related(fairyTale, 0, 5);
            model.SuggestTags = await _suggestedTagRepository.GetRandomAsync(0, 5);

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ManageViewModel> BuildManageViewModel(int id) {
            var fairyTale = await _repository.GetAsync(id);

            var notFormatted = await _repository.GetAllAsync(x => 
                !x.Text.StartsWith("<p>") || !x.Text.Contains(Environment.NewLine), 0, 1);

            var count = await _repository.CountAsync(x => 
                !x.Text.StartsWith("<p>") || !x.Text.Contains(Environment.NewLine));

            var next = notFormatted.FirstOrDefault();

            var model = new ManageViewModel {
                Title = fairyTale.Title,
                Description = fairyTale.Description,
                FairyTale = fairyTale,
                Next = next,
                NextCount = count
            };

            return model;
        }

        private readonly IFairyTaleRepository _repository;
        private readonly IRepository<SuggestedTag> _suggestedTagRepository;
    }

    public interface IFairyTalesBuilder {
        Task<SingleViewModel> BuildSingleViewModel(int id);
        Task<ManageViewModel> BuildManageViewModel(int id);
    }
}