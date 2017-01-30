using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT.Entities;
using FT.MvcApp.FairyTales.Models;
using FT.Repositories;
using FT.Repositories.Fake;

namespace FT.MvcApp.FairyTales.Services
{
    public class FairyTalesService : IFairyTalesService {
        public FairyTalesService(IRepository<FairyTale> repository) {
            _repository = repository;
        }

        private readonly IRepository<FairyTale> _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SingleViewModel> BuildSingleViewModel(long id) {
            var fairyTale = await _repository.GetAsync(id);

            var model = new SingleViewModel
            {
                Title = "Просмотр",
                Description = fairyTale.Description,
                FairyTale = fairyTale
            };

            return model;
        }        
    }

    public interface IFairyTalesService {
        Task<SingleViewModel> BuildSingleViewModel(long id);
    }
}