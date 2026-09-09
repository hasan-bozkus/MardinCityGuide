using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        Task<User> GetUserByNameAsync(string userName);
        Task UpdateUserAvatarUrlAsync(int userId, string avatarUrl);

        Task UpdateUserWithNameAndUserNameAndEmailAsync(int userId, string nameSurname, string userName, string email);
    }
}
