using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("GalleryImages")]
    public class GalleryImage
    {
        [PrimaryKey, AutoIncrement]
        public int GalleryImageId { get; set; }

        [NotNull]
        public string ImageUrl { get; set; } = default!;

        [NotNull]
        public string? AltText { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public bool IsCover { get; set; }

        // Polimorfik ilişki yerine her modül kendi FK'sini tutuyor (aşağıda)
        public int? BazaarId { get; set; }
        public int? MuseumId { get; set; }
        public int? HistoricalSiteId { get; set; }
        public int? PlaceId { get; set; }
        public int? ShopId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
