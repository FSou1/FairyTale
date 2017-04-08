using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FT.Components.Utility;
using FT.Entities;
using FT.Repositories;

namespace FT.MvcApp.FairyTales.Services {
    public class FairyTalesService : IFairyTalesService {
        public FairyTalesService(
            IRepository<FairyTale> repository,
            IRepository<SuggestedTag> suggestedTagRepository
        ) {
            _repository = repository;
            _suggestedTagRepository = suggestedTagRepository;
        }

        public async Task<FairyTale> GetAsync(int id) {
            return await _repository.GetAsync(id);
        }

        public async Task UpdateAsync(FairyTale fairyTale) {
            Guard.ArgumentNotNull(fairyTale, nameof(fairyTale));

            await _repository.UpdateAsync(fairyTale);
        }

        public async Task IncreaseViews(FairyTale fairyTale) {
            Guard.ArgumentNotNull(fairyTale, nameof(fairyTale));

            fairyTale.Summary.Views += 1;
            await _repository.UpdateAsync(fairyTale);
        }

        public async Task<IList<FairyTale>> GetAllAsync(Expression<Func<FairyTale, bool>> filter, int skip, int take) {
            return await _repository.GetAllAsync(filter, skip, take);
        }

        public async Task<FairyTale> GetRandomAsync() {
            var tales = await _repository.GetRandomAsync(0, 1);
            return tales?[0];
        }

        public async Task SuggestTag(FairyTale fairyTale, int suggestedTagId) {
            var tag = await _suggestedTagRepository.GetAsync(suggestedTagId);
            if (tag == null) {
                throw new NullReferenceException(nameof(tag));
            }

            fairyTale.SuggestedTags.Add(tag);

            await UpdateAsync(fairyTale);
        }

        private readonly IRepository<FairyTale> _repository;
        private readonly IRepository<SuggestedTag> _suggestedTagRepository;
    }

    public interface IFairyTalesService {
        Task<FairyTale> GetAsync(int id);

        Task UpdateAsync(FairyTale fairyTale);

        Task IncreaseViews(FairyTale fairyTale);

        Task<IList<FairyTale>> GetAllAsync(
            Expression<Func<FairyTale, bool>> filter, int skip, int take
        );

        Task<FairyTale> GetRandomAsync();

        Task SuggestTag(
            FairyTale fairyTale, int suggestedTagId
        );
    }
}