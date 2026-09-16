using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteHomeNavTileRepository : GenericRepository<HomeNavTile>, IHomeNavTileDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteHomeNavTileRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<HomeNavTile>> GetHomeNavTileListBySortOrderAndIsActiveAsync()
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<HomeNavTile>().Where(x => x.IsActive == true).OrderByDescending(y => y.HomeNavTitleId).ToListAsync();

            return values;
        }

        public async Task<HomeNavTile> GetHomeNawTitleByTitleAsync(string title)
        {
            await _appDatabase.InitAsync();

            var values = await _connection.Table<HomeNavTile>().Where(x => x.Title == title).FirstOrDefaultAsync();
            return values;
        }
    }
}
