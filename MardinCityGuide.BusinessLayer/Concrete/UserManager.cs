using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class UserManager : GenericManager<User>, IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IGenericDal<User> genericDal, IUserDal userDal) : base(genericDal)
        {
            _userDal = userDal;
        }

        public async Task<User> TGetUserByNameAsync(string userName)
        {
            return await _userDal.GetUserByNameAsync(userName);
        }
    }
}
