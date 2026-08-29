using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteSeasonalSpotRepository : GenericRepository<SeasonalSpot>, ISeasonalSpotDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteSeasonalSpotRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<SeasonalSpot>> GetRandom2ReligiousListAsync()
        {
            await _appDatabase.InitAsync();

            var religiousSiteCount = await _connection.Table<ReligiousSite>().Where(x => x.IsActive == true).CountAsync();
            var random = new Random().Next(0, religiousSiteCount - 1);

            var religiousSites = await _connection.Table<ReligiousSite>().Where(x => x.IsActive == true).Skip(random - 1).Take(2).ToListAsync();

            var values = religiousSites.Select(x => new SeasonalSpot
            {
                TagLabel = x.Name,
                IconKey = x.SiteType.ToString()
                
            }).ToList();

            return values;
        }
    }
}
