using System;
using System.Threading.Tasks;
using FT.Entities;
using FT.Repositories;

namespace FT.MvcApp.TagSuggestions.Services {
    public class TagSuggestionService : ITagSuggestionService {
        public TagSuggestionService(IRepository<TagSuggestion> repository) {
            _repository = repository;
        }

        public async Task SuggestTag(int fairyTaleId, int suggestedTagId) {
            var suggestion = new TagSuggestion {
                FairyTaleId = fairyTaleId,
                SuggestedTagId = suggestedTagId,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _repository.CreateAsync(suggestion);
        }

        private readonly IRepository<Entities.TagSuggestion> _repository;
    }

    public interface ITagSuggestionService {
        Task SuggestTag(int fairyTaleId, int suggestedTagId);
    }
}