using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("HomeNavTitles")]
    public class HomeNavTile // "Explore by Soul": Gastronomy, Spirituality, Bazaars, Events
    {
        [PrimaryKey, AutoIncrement]
        public int HomeNavTitleId { get; set; }

        [NotNull]
        public string Title { get; set; } = default!;

        [NotNull]
        public string IconKey { get; set; } = default!;   // ör: "fork-knife", "mosque", "shopping-bag"
        public SectionType TargetSection { get; set; }
        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
