using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("SkyGazingInfo")]
    public class SkyGazingInfo
    {
        [PrimaryKey, AutoIncrement]
        public int SkyGazingInfoId { get; set; }

        public DateTime ForMonth { get; set; }              // hangi ay için geçerli
        public DateTime PeakStart { get; set; }               // 18:42
        public DateTime PeakEnd { get; set; }                  // 19:15

        [NotNull]
        public string VisibilityLabel { get; set; } = default!; // "Perfect Visibility"

        [NotNull]
        public string ForecastUrl { get; set; } = string.Empty;  // "View Forecast"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
