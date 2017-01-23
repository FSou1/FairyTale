using FT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Repositories
{
    public class TagRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tag Get(int id)
        {
            return data.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Tag> GetAll(Func<Tag, int> orderByDescending)
        {
            return data.OrderByDescending(orderByDescending).ToList();
        }

        private static readonly IEnumerable<Tag> data = new List<Tag>()
        {
            new Tag
            {
                Id = 1,
                Title = "Русские-народные",
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
