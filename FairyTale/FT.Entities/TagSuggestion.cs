using System;
using FluentNHibernate.Mapping;
using FT.Entities.Contract;

namespace FT.Entities {
    public class TagSuggestion : IEntity {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual char FirstLetter { get; set; }
        public virtual int FairyTaleId { get; set; }
        public virtual int SuggestedTagId { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
    }

    public class TagSuggestionMap : ClassMap<TagSuggestion> {
        public TagSuggestionMap() {
            Schema("[ft]");
            Table("[TagSuggestions]");

            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.FairyTaleId);
            Map(x => x.SuggestedTagId);
            Map(x => x.CreatedAtUtc);
        }
    }
}