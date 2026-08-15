using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteCulturalEventRepository : GenericRepository<CulturalEvent>, ICulturalEventDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteCulturalEventRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<CulturalEvent>> GetUpcoming2EventsAsync()
        {
            await _appDatabase.InitAsync();

            var today = DateTime.Today;

            var values = await _connection.Table<CulturalEvent>().Where(x => x.EventDate >= today).Take(2).ToListAsync();

            values.Select(x => new
            {
                EventDate = x.EventDate
            }).ToList();

            return values;
        }
    }
}
