using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Bazaars")]
    public class Bazaar
    {
        [PrimaryKey, AutoIncrement]
        public int BazaarId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string? Subtitle { get; set; } = string.Empty;      // "The Coppersmith's Guild"

        [NotNull]
        public string? Description { get; set; } = string.Empty;

        [NotNull]
        public string CoverImageUrl { get; set; } = default!;
        public bool IsFeatured { get; set; }          // "Iconic Bazaars" bölümünde büyük kart

        public decimal? Rating { get; set; }        // YENİ: 4.7
        public int? ReviewCount { get; set; }        // YENİ: 850

        public int LocaitonId { get; set; }

        [Ignore]
        public Location? Location { get; set; }

        public int CategoryId { get; set; }

        [Ignore]
        public Category Category { get; set; } = default!;

        [Ignore]
        public ICollection<GalleryImage>? Images { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
