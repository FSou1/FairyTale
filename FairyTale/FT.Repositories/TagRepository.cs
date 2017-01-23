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
        public Tag Get(int id)
        {
            return data.FirstOrDefault(t => t.Id == id);
        }

        private static readonly IEnumerable<Tag> data = new List<Tag>()
        {
            new Tag
            {
                Id = 1,
                Title = "Русские-народные"
            }
        };
    }
}
