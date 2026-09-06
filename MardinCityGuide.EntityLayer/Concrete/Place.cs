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
        public string TypeLabel { get; set; } = string.Empty;          // "Geleneksel Mardin Mutfağı", "Teraslı Kafe"

        [NotNull]
        public string Description { get; set; } = string.Empty;
        public PlaceType PlaceType { get; set; }
        public PriceLevel PriceLevel { get; set; }
        public decimal? Rating { get; set; }             // 4.9 / 4.7 ...
        public bool IsFeatured { get; set; }             // en üstteki büyük kart
        public bool IsFavorite { get; set; }             // en üstteki büyük kart

        public TimeSpan? OpeningTimeStart { get; set; }         // 09:00
        public TimeSpan? OpeningTimeEnd { get; set; }           // 18:00

        [NotNull]
        public string TastingMenuUrl { get; set; } = string.Empty;      // "Tadım Menüsünü Görüntüle"

        public int? ReviewCount { get; set; }        // YENİ: 1.2k / 850
        public string? Tag { get; set; }              // YENİ: "ÜST DÜZEY YEMEK DENEYİMİ", "OTANTİK", "TERAS MANZARASI", "YEREL HALKIN GÖZDESİ"

        public int LocationId { get; set; }

        [Ignore]
        public Location Location { get; set; } = default!;

        public int CategoryId { get; set; }              // Tüm Mekanlar/Geleneksel/Teraslı...

        [Ignore]
        public Category Category { get; set; } = default!;

        [Ignore]
        public ICollection<GalleryImage>? Images { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
