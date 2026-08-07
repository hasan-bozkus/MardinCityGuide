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

        public async Task<Place> GetPlaceWithLocationAndCategoryAsync(int id)
        {
            await _appDatabase.InitAsync();

            var place = await _connection.Table<Place>().Where(x=> x.PlaceId == id).FirstOrDefaultAsync();

            var category = await _connection.Table<Category>().Where(y => y.CategoryId == place.CategoryId).FirstOrDefaultAsync();
            var location = await _connection.Table<EntityLayer.Concrete.Location>().Where(y => y.LocationId == place.LocationId).FirstOrDefaultAsync();

            place!.Location = location;
            place!.Category = category;

            return place;
        }

        public async Task<Place> GetRandomPlaceAsync()
        {
            var placeCount = await _connection.Table<Place>().CountAsync();
            var random = new Random().Next(0, placeCount);
            var value = await _connection.Table<Place>().Skip(random).FirstOrDefaultAsync();
            return value;
        }
    }
}
