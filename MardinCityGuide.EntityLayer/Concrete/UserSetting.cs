using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.EntityLayer.Concrete
{

    [Table("UserSettings")]
    public class UserSetting
    {
        [PrimaryKey, AutoIncrement]
        public int UserSettingId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public bool NotificationsEnabled { get; set; } = true;

        [NotNull]
        public string Language { get; set; } = "tr-TR"; //en-US

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
