using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{

    [Table("Highlights")]
    public class Highlight // Ana sayfa "Featured" carousel: Deyrulzafaran, Zinciriye Medresesi...
    {
        [PrimaryKey, AutoIncrement]
        public int HighlightId { get; set; }

        [NotNull]
        public string Title { get; set; } = default!;

        [NotNull]
        public string Subtitle { get; set; } = string.Empty;          // "Ancient Saffron Monastery"

        [NotNull]
        public string ImageUrl { get; set; } = default!;

        [NotNull]
        public string BadgeLabel { get; set; } = string.Empty;         // "Must Visit"
        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Hangi entity'e yönlendirdiği
        public FavoriteType TargetType { get; set; }
        public int TargetId { get; set; }
    }
}
