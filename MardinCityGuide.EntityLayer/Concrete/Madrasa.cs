using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Madrasas")]
    public class Madrasa
    {
        [PrimaryKey, AutoIncrement]
        public int MadrasaId { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string ImageUrl { get; set; } = default!;

        [NotNull]
        public string Era { get; set; } = string.Empty;              // "Artuqid Era, 1385"

        [NotNull]
        public string Dynasty { get; set; } = string.Empty;            // "Artuqid" (ayrı da tutulabilir)

        public Location? Location { get; set; }

        // Favorilere eklenebilir (kalp ikonu)
        public bool IsFavorite { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
