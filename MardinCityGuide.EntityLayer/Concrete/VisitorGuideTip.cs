using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("VisitorGuideTips")]
    public class VisitorGuideTip     // "Dress Code", "Best Time to Visit" gibi ziyaretçi ipuçları
    {
        [PrimaryKey, AutoIncrement]
        public int VisitorGuideTipId { get; set; }

        [NotNull] 
        public string IconKey { get; set; } = default!;   // "info", "calendar"

        [NotNull]
        public string Title { get; set; } = default!;

        [NotNull]
        public string Description { get; set; } = default!;
        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
