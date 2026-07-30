using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("HistorialSites")]
    public class HistoricalSite // "Ancient Dara / Great Necropolis" gibi antik alanlar
    {
        [PrimaryKey, AutoIncrement]
        public int HistoricalsiteId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string Subtitle { get; set; } = string.Empty;           // "The Mesopotamia's Ephesus"

        [NotNull]
        public string CoverImageUrl { get; set; } = default!;
        public bool IsSignatureExperience { get; set; }
        public bool IsBookable { get; set; }

        [NotNull]
        public string BookingUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public Location Location { get; set; } = default!;
        public ICollection<GalleryImage>? Images { get; set; }


    }
}
