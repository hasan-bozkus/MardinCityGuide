using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("ArtisanCrafts")]
    public class ArtisanCraft     // "Artisanal Mastery" bölümü: Telkari Silver, Bıttım Soap
    {
        [PrimaryKey, AutoIncrement]
        public int ArtisonCraftId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string Description { get; set; } = default!;

        [NotNull]
        public string ImageUrl { get; set; } = default!;
        public CraftActionType ActionType { get; set; }

        [NotNull]
        public string? ActionUrl { get; set; }        // Discover Process / Where to Buy linki

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
