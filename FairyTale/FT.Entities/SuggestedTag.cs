using FluentNHibernate.Mapping;
using FT.Entities.Contract;

namespace FT.Entities {
    public class SuggestedTag : IEntity {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual char FirstLetter { get; set; }
    }

    public class SuggestedTagMap : ClassMap<SuggestedTag> {
        public SuggestedTagMap() {
            Schema("[ft]");
            Table("[SuggestedTags]");

            Id(x => x.Id);
            Map(x => x.Title);
        }
    }
}