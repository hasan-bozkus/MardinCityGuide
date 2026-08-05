using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteEditorialHighlightRepository : GenericRepository<EditorialHighlight>, IEditorialHighlightDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteEditorialHighlightRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<EditorialHighlight> GetRandomEditoralHighlightAsync()
        {
            await _appDatabase.InitAsync();

            var highlightCount = await _connection.Table<EditorialHighlight>().CountAsync();

            var random = new Random().Next(1, highlightCount);

            var value = await _connection.Table<EditorialHighlight>().FirstOrDefaultAsync(X => X.EditoralHighlightId == random);

            return value;
        }
    }
}
