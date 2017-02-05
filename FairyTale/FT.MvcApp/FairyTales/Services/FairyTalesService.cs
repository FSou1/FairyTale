using System;
using System.Threading.Tasks;
using FT.Components.Utility;
using FT.Entities;
using FT.Repositories;

namespace FT.MvcApp.FairyTales.Services {
    public class FairyTalesService : IFairyTalesService {
        public FairyTalesService(IRepository<FairyTale> repository) {
            _repository = repository;
        }

        public async Task<FairyTale> GetAsync(int id) {
            return await _repository.GetAsync(id);
        }

        public async Task UpdateAsync(FairyTale fairyTale) {
            Guard.ArgumentNotNull(fairyTale, nameof(fairyTale));

            await _repository.UpdateAsync(fairyTale);
        }

        private readonly IRepository<FairyTale> _repository;
    }

    public interface IFairyTalesService {
        Task<FairyTale> GetAsync(int id);

        Task UpdateAsync(FairyTale fairyTale);
    }
}