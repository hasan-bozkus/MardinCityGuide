using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Favorites")]
    public class Favorite // // Polimorfik favori kaydı: TargetId, FavoriteType'a göre ilgili tabloya işaret eder
    {
        [PrimaryKey, AutoIncrement]
        public int FavoriteId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public FavoriteType FavoriteType { get; set; }
        public int TargetId { get; set; }
    }
}
