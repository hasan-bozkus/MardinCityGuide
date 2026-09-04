using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.Mobile.Dtos.UserDtos
{
    public class RegisterUserDtos
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ImageUrl { get; set; }
    }
}
