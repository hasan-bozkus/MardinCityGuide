using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("CulturalEvents")]
    public class CulturalEvent
    {
        [PrimaryKey, AutoIncrement]
        public int CulturalEventId { get; set; }

        [NotNull]
        public string Title { get; set; } = default!;
        public DateTime EventDate { get; set; }         // gün/ay ayrı da gösterilebilir (14 / OCT)
        public DateTime? EventEndDate { get; set; }        // YENİ: "June 12-15" aralığı için


        [NotNull]
        public string LocationName { get; set; } = default!; // "Old Market District"

        [NotNull]
        public string DetailUrl { get; set; } = string.Empty;

        [NotNull]
        public string Description { get; set; } = string.Empty;             // YENİ: "A cross-cultural journey..."

        [NotNull]
        public string CoverImageUrl { get; set; } = string.Empty;            // YENİ: Featured banner görseli
        public bool IsFeatured { get; set; }                  // YENİ: "FEATURED" rozeti

        [NotNull]
        public string ExploreProgramUrl { get; set; } = string.Empty;        // YENİ: "Explore Program" linki

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
