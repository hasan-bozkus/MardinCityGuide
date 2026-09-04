using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.Mobile.Dtos.UserDtos
{
    public class LoginUserDto
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
