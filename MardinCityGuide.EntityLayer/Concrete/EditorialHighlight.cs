using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("EditoralHighlights")]
    public class EditorialHighlight // "Sunset Tea at the Great Mosque" - AUTHENTIC CHOICE kartı
    {
        [PrimaryKey, AutoIncrement]
        public int EditoralHighlightId { get; set; }

        [NotNull]
        public string BadgeLabel { get; set; } = default!;   // "AUTHENTIC CHOICE"

        [NotNull]
        public string Title { get; set; } = default!;

        [NotNull]
        public string Description { get; set; } = default!;

        [NotNull]
        public string IllustrationUrl { get; set; } = string.Empty;

        [NotNull]
        public string CtaLabel { get; set; } = default!;      // "Explore Details"

        [NotNull]
        public string CtaUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
