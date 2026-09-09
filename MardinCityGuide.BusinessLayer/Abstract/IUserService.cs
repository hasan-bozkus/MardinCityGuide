using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> TGetUserByNameAsync(string userName);
        Task TUpdateUserAvatarUrlAsync(int userId, string avatarUrl);
        Task TUpdateUserWithNameAndUserNameAndEmailAsync(int userId, string nameSurname, string userName, string email);

    }
}
