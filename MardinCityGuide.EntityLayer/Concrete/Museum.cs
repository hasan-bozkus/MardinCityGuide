using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Museums")]
    public class Museum
    {
        [PrimaryKey, AutoIncrement]
        public int MuseumId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string CoverImageUrl { get; set; } = default!;
        public TimeSpan? OpeningTime { get; set; }     // "Open • 09:00"
        public bool IsOpenNow { get; set; }
        public bool IsFeatured { get; set; }

        public int? LocationId { get; set; }
        public Location? Location { get; set; } = default!;
        public ICollection<GalleryImage>? Images { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
