using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.Mobile.Models
{
    public class CurrentSession
    {
        public static int UserId { get; set; }
        public static string NameSurname { get; set; } = string.Empty;
        public static string UserName { get; set; } = string.Empty;
        public static string Email { get; set; } = string.Empty;
        public static string? AvatarUrl { get; set; } = string.Empty;

        public static void Clear()
        {
            UserId = 0;
            NameSurname = string.Empty;
            UserName = string.Empty;
            Email = string.Empty;
            AvatarUrl = string.Empty;
        }
    }
}
