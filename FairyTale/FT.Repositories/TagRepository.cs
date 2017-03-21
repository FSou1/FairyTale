using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT.Entities;
using NHibernate.Linq;

namespace FT.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public Task<IList<char>> GetTitleFirstLetters(bool distinct)
        {
            var query = Session.Query<Tag>().Select(x => x.FirstLetter);
            var letters = (distinct ? query.Distinct() : query).ToList();
            return Task.FromResult<IList<char>>(letters);
        }
    }

    public interface ITagRepository : IRepository<Tag>
    {
        Task<IList<char>> GetTitleFirstLetters(bool distinct);
    }
}