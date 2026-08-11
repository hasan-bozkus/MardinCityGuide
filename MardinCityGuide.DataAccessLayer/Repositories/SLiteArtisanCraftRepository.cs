using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteArtisanCraftRepository : GenericRepository<ArtisanCraft>, IArtisanCraftDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteArtisanCraftRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<ArtisanCraft>> GetIsActiveArtisanCraftListAsync()
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<ArtisanCraft>().Where(x => x.IsActive).ToListAsync();
            return values;
        }

        public async Task<List<ArtisanCraft>> GetRandom2ArtisanCraftWithCategoryAsycn()
        {
            await _appDatabase.InitAsync();

            var artisanCraftCount = await _connection.Table<ArtisanCraft>().CountAsync();

            if (artisanCraftCount == 0)
                return new List<ArtisanCraft>();

            if (artisanCraftCount <= 2)
                return await _connection.Table<ArtisanCraft>().ToListAsync();

            var rondam = new Random().Next(0, artisanCraftCount - 1);

            var values = await _connection.Table<ArtisanCraft>().Skip(rondam - 1).Take(2).ToListAsync();

            return values;
        }
    }
}
