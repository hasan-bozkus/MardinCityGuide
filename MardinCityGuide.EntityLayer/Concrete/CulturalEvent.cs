using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("CulturalEvents")]
    public class CulturalEvent
    {
        [PrimaryKey, AutoIncrement]
        public int CulturalEventId { get; set; }

        [NotNull]
        public string Title { get; set; } = default!;
        public DateTime EventDate { get; set; }         // gün/ay ayrı da gösterilebilir (14 / OCT)

        [NotNull]
        public string LocationName { get; set; } = default!; // "Old Market District"

        [NotNull]
        public string DetailUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
