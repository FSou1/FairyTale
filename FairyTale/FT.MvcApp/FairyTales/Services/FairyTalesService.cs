using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<IList<FairyTale>> GetAllAsync(Expression<Func<FairyTale, bool>> filter, int skip, int take) {
            return await _repository.GetAllAsync(filter, skip, take);
        }

        public async Task<FairyTale> GetRandomAsync() {
            var tales = await _repository.GetRandomAsync(0, 1);
            return tales?[0];
        }

        private readonly IRepository<FairyTale> _repository;
    }

    public interface IFairyTalesService {
        Task<FairyTale> GetAsync(int id);

        Task UpdateAsync(FairyTale fairyTale);

        Task<IList<FairyTale>> GetAllAsync(
            Expression<Func<FairyTale, bool>> filter, int skip, int take
        );

        Task<FairyTale> GetRandomAsync();
    }
}