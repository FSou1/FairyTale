using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Entities {
    public class FairyTale {
        public int Id { get; set; }        
        public string Title { get; set; }
        public string Text { get; set; }
        public IList<Tag> Tags { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
