using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FT.Entities;

namespace FT.Repositories.Fake
{
    public class FakeTagRepository : IRepository<Tag>
    {
        public Task<IList<Tag>> GetAllAsync(
            Expression<Func<Tag, bool>> filter, int skip, int take)
        {
            var result = data.Where(filter.Compile()).Skip(skip).Take(take).ToList();
            return Task.FromResult<IList<Tag>>(result);
        }

        public Task<IList<Tag>> GetAllAsync<TKey>(
            Expression<Func<Tag, bool>> filter, Func<Tag, TKey> orderBy, int skip, int take)
        {
            var result = data.Where(filter.Compile()).OrderBy(orderBy).Skip(skip).Take(take).ToList();
            return Task.FromResult<IList<Tag>>(result);
        }

        public Task<IList<Tag>> GetAllAsync<TKey>(
            Func<Tag, TKey> orderBy)
        {
            var result = data.OrderBy(orderBy).ToList();
            return Task.FromResult<IList<Tag>>(result);
        }

        public Task<Tag> GetAsync(object id)
        {
            var result = data.FirstOrDefault(f => f.Id == (int)id);
            return Task.FromResult(result);
        }

        public Task<int> CountAsync()
        {
            var result = data.Count();
            return Task.FromResult(result);
        }

        public Task<int> CountAsync(Expression<Func<Tag, bool>> filter)
        {
            var result = data.Count(filter.Compile());
            return Task.FromResult(result);
        }
        

        private static readonly IEnumerable<Tag> data = new List<Tag>()
        {
            new Tag
            {
                Id = 1,
                Title = "Русские-народные",
                Description = "Многообразие. Именно этим словом можно описать собранные здесь произведения. Многообразие сюжетов, персоонажей, традиций. Русские народные сказки, будь то волшебные, мифологические или бытовые, отражают быт, нравы и поверья русского человека. Идеализирующие дружбу и самопожертвование, или изобличающие пороки - все они неотъемлимая часть нашей истории.",
                FairyTalesCount = 4
            },
            new Tag
            {
                Id = 2,
                Title = "Сборник Незнайка",
                FairyTalesCount = 8
            },
            new Tag
            {
                Id = 3,
                Title = "А.С. Пушкин",
                FairyTalesCount = 33
            },
            new Tag
            {
                Id = 4,
                Title = "Про животных",
                FairyTalesCount = 198
            }
        };
    }
}
