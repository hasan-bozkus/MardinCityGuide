using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.EntityLayer.Concrete;

namespace MardinCityGuide.DataAccessLayer.Concrete
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _connection;
        private bool _isInitialized = false;

        public AppDatabase(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public async Task InitAsync()
        {
            if (_isInitialized)
                return;

            await _connection.CreateTableAsync<Category>();

            _isInitialized = true;
        }
    }
}
