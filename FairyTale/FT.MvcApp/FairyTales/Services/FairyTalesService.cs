using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT.Entities;
using FT.MvcApp.FairyTales.Models;
using FT.Repositories;

namespace FT.MvcApp.FairyTales.Services
{
    public class FairyTalesService : IFairyTalesService {
        public FairyTalesService(IRepository<FairyTale> repository) {
            _repository = repository;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FairyTale> _repository;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<SingleViewModel> BuildDirtyViewModel() {
            var fairyTale = (await _repository.GetAllAsync(f => !f.Text.StartsWith("<p>"), 0, 1)).FirstOrDefault();

            var model = new SingleViewModel {
                Title = fairyTale.Title,
                Description = fairyTale.Description,
                FairyTale = fairyTale
            };

            return model;
        }
    }

    public interface IFairyTalesService {
        Task<SingleViewModel> BuildSingleViewModel(int id);
        Task<SingleViewModel> BuildDirtyViewModel();
    }
}