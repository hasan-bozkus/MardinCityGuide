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

        public async Task GetChangeIsFavoriteStatusAsync(int id)
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<Place>().Where(x => x.PlaceId == id).FirstOrDefaultAsync();

            if (values != null)
            {
                values.IsFavorite = !values.IsFavorite;
                await _connection.UpdateAsync(values);
            }
        }

        public async Task GetChangeIsFavoriteStatusFalseAsync(int id)
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<Place>().Where(x => x.PlaceId == id).FirstOrDefaultAsync();

            if (values != null)
            {
                values.IsFavorite = false;
                await _connection.UpdateAsync(values);
            }
        }

        public async Task GetChangeIsFavoriteStatusTrueAsync(int id)
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<Place>().Where(x => x.PlaceId == id).FirstOrDefaultAsync();

            if (values != null)
            {
                values.IsFavorite = true;
                await _connection.UpdateAsync(values);
            }
        }

        public async Task<Place> GetHighestStarredPlaceWithLocationAsync()
        {
            await _appDatabase.InitAsync();

            var getHighsetStarredPlace = await _connection.Table<Place>().Where(y => y.IsActive == true && y.IsFeatured == true).OrderBy(x => (double)x.Rating).FirstOrDefaultAsync();
            var location = await _connection.Table<EntityLayer.Concrete.Location>().Where(x => x.LocationId == getHighsetStarredPlace.LocationId).FirstOrDefaultAsync();

            getHighsetStarredPlace.Location = location;

            return getHighsetStarredPlace;
        }

        public async Task<List<Place>> GetPlaceListWithLocationByIsActiveAndIsFeatuderAsync()
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<Place>().Where(x => x.IsActive == true && x.IsFeatured == true).ToListAsync();

            foreach (var place in values)
            {
                var location = await _connection.Table<EntityLayer.Concrete.Location>().Where(x => x.LocationId == place.LocationId).FirstOrDefaultAsync();
                place.Location = location;
            }
            return values;
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
