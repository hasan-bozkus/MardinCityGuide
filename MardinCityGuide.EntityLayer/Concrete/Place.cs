using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Places")]
    public class Place // "Cercis Murat Konağı", "Seyri Merdin", "Shiluh Winery", "Leyli Muse Mutfak"
    {
        [PrimaryKey, AutoIncrement]
        public int PlaceId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string CoverImageUrl { get; set; } = default!;

        [NotNull]
        public string TypeLabel { get; set; } = string.Empty;          // "Traditional Mardin Cuisine", "Terraced Cafe"
        public PlaceType PlaceType { get; set; }
        public PriceLevel PriceLevel { get; set; }
        public decimal? Rating { get; set; }             // 4.9 / 4.7 ...
        public bool IsFeatured { get; set; }             // en üstteki büyük kart

        [NotNull]
        public string TastingMenuUrl { get; set; } = string.Empty;      // "View Tasting Menu"

        public int? ReviewCount { get; set; }        // YENİ: 1.2k / 850
        public string? Tag { get; set; }              // YENİ: "FINE DINING", "AUTHENTIC", "TERRACE VIEW", "LOCAL FAVORITE"

        public int LocationId { get; set; }

        [Ignore]
        public Location Location { get; set; } = default!;

        public int CategoryId { get; set; }              // All Places/Traditional/Terraced...

        [Ignore]
        public Category Category { get; set; } = default!;

        [Ignore]
        public ICollection<GalleryImage>? Images { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
