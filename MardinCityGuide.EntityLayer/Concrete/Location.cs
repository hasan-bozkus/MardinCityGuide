using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Locations")]
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int LocationId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [NotNull]
        public string AddressText { get; set; } = string.Empty;   // "Old Town Main St."

        [NotNull]
        public string District { get; set; } = string.Empty;       // "Artuklu", "Old City Center"
        public double? DistanceKmFromUser { get; set; } // "0.2 km", "30km from City"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
