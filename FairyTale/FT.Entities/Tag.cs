using System.Collections.Generic;

namespace FT.Entities {
    public class Tag {
        public int Id { get; set; }
        public string Title { get; set; }
        public int FairyTalesCount { get; set; }

        public IList<FairyTale> FairyTales { get; set; }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();            
        }
    }
}