using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteReligiousSiteRepository : GenericRepository<ReligiousSite>, IReligiousSiteDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteReligiousSiteRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<ReligiousSite>> GetMosquesAndMonasteriesListAsync()
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<ReligiousSite>().Where(x => x.IsActive == true).OrderBy(y => y.Rating).ToListAsync();
            return values;
        }
    }
}
