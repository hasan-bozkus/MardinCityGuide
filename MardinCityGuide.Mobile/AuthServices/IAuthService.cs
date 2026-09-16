using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.DataAccessLayer.Dtos.UserDtos;

namespace MardinCityGuide.Mobile.AuthServices
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterUserDtos registerUserDtos);
        Task<bool> LoginAsync(string userName, string password);
    }
}
