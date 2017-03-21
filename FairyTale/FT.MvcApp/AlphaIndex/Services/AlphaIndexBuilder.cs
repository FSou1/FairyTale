using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT.Entities;
using FT.MvcApp.AlphaIndex.Models;
using FT.Repositories;

namespace FT.MvcApp.AlphaIndex.Services
{
    public class AlphaIndexBuilder : IAlphaIndexBuilder
    {
        public AlphaIndexBuilder(
            IFairyTaleRepository taleRepository,
            ITagRepository tagRepository
        )
        {
            _taleRepository = taleRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IndexViewModel> BuildIndexModel()
        {
            var talesLetters = await _taleRepository.GetTitleFirstLetters(true);
            var tagsLetters = await _tagRepository.GetTitleFirstLetters(true);

            var model = new IndexViewModel {
                TalesLetters = talesLetters,
                TagsLetters = tagsLetters
            };

            return model;
        }


        private readonly IFairyTaleRepository _taleRepository;
        private readonly ITagRepository _tagRepository;
    }

    public interface IAlphaIndexBuilder
    {
        Task<IndexViewModel> BuildIndexModel();
    }
}