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
