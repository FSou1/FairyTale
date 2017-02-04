using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT.Entities;
using FT.MvcApp.FairyTales.Models;
using FT.Repositories;

namespace FT.MvcApp.FairyTales.Services
{
    public class FairyTalesBuilder : IFairyTalesBuilder {
        public FairyTalesBuilder(IRepository<FairyTale> repository) {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SingleViewModel> BuildSingleViewModel(int id) {
            var fairyTale = await _repository.GetAsync(id);

            var model = new SingleViewModel
            {
                Title = fairyTale.Title,
                Description = fairyTale.Description,
                FairyTale = fairyTale
            };

            return model;
        }

        private readonly IRepository<FairyTale> _repository;
    }

    public interface IFairyTalesBuilder {
        Task<SingleViewModel> BuildSingleViewModel(int id);
    }
}