using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteRouteRepository : GenericRepository<Route>, IRouteDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteRouteRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<Route> GetOneRandomRouteAsync()
        {
            await _appDatabase.InitAsync();

            var routecount = await _connection.Table<Route>().Where(x=> x.IsActive == true).CountAsync();
            var random = new Random().Next(0, routecount);

            var value = await _connection.Table<Route>().Where(x => x.IsActive == true).Skip(random - 1).Take(1).FirstOrDefaultAsync();


            return value;
        }
    }
}
