using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Routes")]
    public class Route // "Curated Itineraries", "Old City Walking Route"
    {
        [PrimaryKey, AutoIncrement]
        public int RouteId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string Description { get; set; } = string.Empty;

        [NotNull]
        public string CoverImageUrl { get; set; } = string.Empty;
        public RouteType RouteType { get; set; }
        public int? EstimatedDurationMinutes { get; set; }

        public RouteCategory? Category { get; set; }           // YENİ: Historical = 1, Culinary = 2, Photography = 3

        [NotNull]
        public string DurationLabel { get; set; } = string.Empty;             // YENİ: "8 HOURS"
        public double? DistanceKm { get; set; }                   // YENİ: 2.4

        [NotNull]
        public string MapUrl { get; set; } = string.Empty;                        // YENİ: "View Map"
        public decimal? Rating { get; set; }                       // YENİ: Wine Route "4.9"
        public int? StopCountLabelOverride { get; set; }         // YENİ: "6 Expert Culinary Stops" gibi özel etiket için   

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        [Ignore]
        public ICollection<RouteStop>? Stops { get; set; } // Restaurant = 1, ReligiousSite = 2, Route = 3

    }
}
