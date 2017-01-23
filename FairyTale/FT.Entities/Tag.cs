using System.Collections.Generic;

namespace FT.Entities {
    public class Tag {
        public int Id { get; set; }
        public string Title { get; set; }

        public IList<FairyTale> FairyTales { get; set; }
    }
}