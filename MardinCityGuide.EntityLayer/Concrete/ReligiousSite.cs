using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("ReligiousSites")]
    public class ReligiousSite
    {
        [PrimaryKey, AutoIncrement]
        public int ReligiousSiteId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string ShortDescription { get; set; } = default!;

        public ReligiousSiteType SiteType { get; set; }        // rozet: MEDRESE / MONASTERY / MOSQUE

        [NotNull]
        public string ImageUrl { get; set; } = default!;

        [NotNull]
        public string Era { get; set; } = string.Empty;              // "Artuqid Era, 1385"

        [NotNull]
        public string Dynasty { get; set; } = string.Empty;            // "Artuqid" (ayrı da tutulabilir)

        public decimal? Rating { get; set; } // 4.9 / 4.8 / 5.0

        public TimeSpan? OpeningTimeStart { get; set; }         // 09:00
        public TimeSpan? OpeningTimeEnd { get; set; }           // 18:00
        public bool IsOpenDaily { get; set; }                   // "Open Daily"

        public decimal? EntryFeeAmount { get; set; }            // 25, 40
        public string? EntryFeeCurrency { get; set; } = "TRY";
        public bool IsFreeEntry { get; set; }                   // Grand Mosque: "Free"

        public Location? Location { get; set; }

        // Favorilere eklenebilir (kalp ikonu)
        public bool IsFavorite { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public string? FullDescription { get; set; }     // YENİ: "About" metni (ShortDescription zaten vardı, uzun açıklama eksikti)
        public VisitInfo? VisitInfo { get; set; }          // YENİ: owned type
        public int? ReviewCount { get; set; }               // YENİ: "(124)"
    }
}
