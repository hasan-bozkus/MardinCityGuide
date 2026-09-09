using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteUserRepository : GenericRepository<User>, IUserDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteUserRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<User>().Where(u => u.UserName == userName).FirstOrDefaultAsync();
            return values;
        }

        public async Task UpdateUserAvatarUrlAsync(int userId, string avatarUrl)
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<User>().Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (values != null)
            {
                values.AvatarUrl = avatarUrl;
                await _connection.UpdateAsync(values);
            }
        }

        public async Task UpdateUserWithNameAndUserNameAndEmailAsync(int userId, string nameSurname, string userName, string email)
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<User>().Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (values != null)
            {
                values.NameSurname = nameSurname;
                values.UserName = userName;
                values.Email = email;
                await _connection.UpdateAsync(values);
            }
        }
    }
}
