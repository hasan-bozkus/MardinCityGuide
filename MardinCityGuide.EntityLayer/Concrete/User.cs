using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [NotNull]
        public string NameSurname { get; set; } = string.Empty;

        [NotNull]
        public string UserName { get; set; } = string.Empty;

        [NotNull]
        public string Email { get; set; } = string.Empty;

        [NotNull]
        public string Password { get; set; } = string.Empty;

        [NotNull]
        public string AvatarUrl { get; set; } = string.Empty;

        public string? DisplayName { get; set; } = "DisplayName";

        public string? PreferredLanguage { get; set; } = "Türkçe (TR)";  // YENİ
        public string? MembershipTier { get; set; }                          // YENİ: "Mardin Guide Premium"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        [Ignore]
        public ICollection<Favorite>? Favorites { get; set; }

        [Ignore]
        public ICollection<Route>? SavedRoutes { get; set; }                 // YENİ: "12 ROUTES" sayacı için ilişki

        // Not: "24 FAVORITES" / "12 ROUTES" ekranda gösterilen sayaçlar,
        // DB'de saklanmaz — Favorites.Count() / SavedRoutes.Count() olarak hesaplanır (Service/DTO katmanında).


    }
}
