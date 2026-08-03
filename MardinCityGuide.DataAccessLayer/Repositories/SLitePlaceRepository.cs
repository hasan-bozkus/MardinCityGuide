using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLitePlaceRepository : GenericRepository<Place>, IPlaceDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLitePlaceRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<Place>> GetPlaceListWithSortOrderAsync()
        {
            await _appDatabase.InitAsync();
            var values = await _connection.Table<Place>().Where(x => x.IsFeatured == true).OrderByDescending(y => y.PlaceId).ToListAsync();
            return values;
        }
    }
}
