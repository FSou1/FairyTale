using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }

    public interface IFairyTaleRepository : IRepository<FairyTale>
    {
        Task<IList<char>> GetTitleFirstLetters(bool distinct);
    }
}