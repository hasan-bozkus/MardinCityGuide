using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteHighlightRepository : GenericRepository<Highlight>, IHighlightDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;


        public SLiteHighlightRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<Highlight>> GetHighlightListBySortOrderAsync()
        {
            await _appDatabase.InitAsync();
            var values = await _connection.Table<Highlight>().OrderBy(x => x.SortOrder).OrderByDescending(x => x.SortOrder).ToListAsync();
            return values;
        }
    }
}
