using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("SeasonalSpots")]
    public class SeasonalSpot
    {
        [PrimaryKey, AutoIncrement]
        public int SeasonalSpotId { get; set; }

        [NotNull]
        public string TagLabel { get; set; } = default!;      // "BEST TERRACE VIEW"

        [NotNull]
        public string IconKey { get; set; } = default!;         // "map-pin", "camera"

        public FavoriteType TargetType { get; set; }
        public int TargetId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
