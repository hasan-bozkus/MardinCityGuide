using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteMuseumRepository : GenericRepository<Museum>, IMuseumDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteMuseumRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }
    }
}
