using System;
using System.Threading.Tasks;
using FT.Entities;
using FT.MvcApp.Utility;
using FT.Repositories;

namespace FT.MvcApp.FairyTales.Services {
    public class FairyTalesService : IFairyTalesService {
        public FairyTalesService(IRepository<FairyTale> repository) {
            _repository = repository;
        }

        public Task UpdateTextAsync(FairyTale fairyTale, UpdateTextOptions options) {
            Guard.ArgumentNotNull(fairyTale, nameof(fairyTale));

            fairyTale.Text = "1" + fairyTale.Text;

            return Task.CompletedTask;
        }

        public async Task<FairyTale> GetAsync(int id) {
            return await _repository.GetAsync(id);
        }

        private readonly IRepository<FairyTale> _repository;
    }

    public interface IFairyTalesService {
        Task UpdateTextAsync(FairyTale fairyTale, UpdateTextOptions options);
        Task<FairyTale> GetAsync(int id);
    }

    public enum UpdateTextOptions {
        AddNewLines
    }
}