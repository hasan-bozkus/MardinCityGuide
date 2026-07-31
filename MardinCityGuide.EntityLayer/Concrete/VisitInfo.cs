using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("VisitInfos")]
    public class VisitInfo     // "Duration 1.5-2h / Price: Free / Era: 12th Century" çipleri
    {
        [PrimaryKey, AutoIncrement]
        public int VisitInfoId { get; set; }

        [NotNull]
        public string DurationLabel { get; set; } = string.Empty;     // "1.5 - 2h"

        [NotNull]
        public string PriceLabel { get; set; } = string.Empty;          // "Free", "25 TRY"

        [NotNull]
        public string EraLabel { get; set; } = string.Empty;             // "12th Century"
        public string IconKey { get; set; } = default!;   // "info", "calendar"
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Polimorfik hedef (ReligiousSite / HistoricalSite / Museum / Place)
        public FavoriteType TargetType { get; set; }
        public int TargetId { get; set; }
    }
}
