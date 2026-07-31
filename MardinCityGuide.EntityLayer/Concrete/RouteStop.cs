using MardinCityGuide.EntityLayer.Enums;
using SQLite;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("RouteStop")]
    public class RouteStop // Rotanın adım adım durakları (ileride detay ekranı için)
    {
        [PrimaryKey, AutoIncrement]
        public int RouteStopId { get; set; }

        public int RouteId { get; set; }

        [Ignore]
        public Route Route { get; set; } = default!;

        public int SortOrder { get; set; }

        [NotNull]
        public string StopName { get; set; } = default!;

        [NotNull]
        public string ImageUrl { get; set; } = string.Empty;                  // YENİ

        [NotNull]
        public string Description { get; set; } = string.Empty;                  // YENİ: "Start your morning with..."
        public TimeSpan? StartTime { get; set; }                   // YENİ: 09:00
        public TimeSpan? EndTime { get; set; }                      // YENİ: 10:30

        [NotNull]
        public string BestTimeLabel { get; set; } = string.Empty;  // YENİ: "Best at 10:00 AM" (Photography Route)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Hangi tabloya bağlı olduğu (opsiyonel, polimorfik referans)
        public FavoriteType? TargetType { get; set; }
        public int? TargetId { get; set; }
    }
}