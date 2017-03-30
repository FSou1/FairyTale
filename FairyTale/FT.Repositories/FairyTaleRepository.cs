using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT.Components.Utility;
using FT.Entities;
using NHibernate.Linq;

namespace FT.Repositories
{
    public class FairyTaleRepository : Repository<FairyTale>, IFairyTaleRepository
    {
        public FairyTaleRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public Task<IList<char>> GetTitleFirstLetters(bool distinct)
        {
            var query = Session.Query<FairyTale>().Select(x => x.FirstLetter);
            var letters = (distinct ? query.Distinct() : query).ToList();
            return Task.FromResult<IList<char>>(letters);
        }

        public async Task<IList<FairyTale>> Related(FairyTale tale, int skip, int take) {
            if (tale == null) return null;

            var tagIds = tale.Tags.Select(t => t.Id);

            var filter = PredicateBuilder.True<FairyTale>()
                .And(x => x.Parent == null)
                .And(x => x.Tags.Any(t => tagIds.Contains(t.Id)))
                .And(x => x.Id > tale.Id);

            return await GetAllAsync(filter, x => x.Id, true, skip, take);
        }
    }

    public interface IFairyTaleRepository : IRepository<FairyTale>
    {
        Task<IList<char>> GetTitleFirstLetters(bool distinct);
        Task<IList<FairyTale>> Related(FairyTale tale, int skip, int take);
    }
}