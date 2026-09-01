using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteRouteStopRepository : GenericRepository<RouteStop>, IRouteStopDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteRouteStopRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<RouteStop>> GetRouteStopsByRouteIdAsync(int id)
        {
            await _appDatabase.InitAsync();

            var routeStops = await _connection.Table<RouteStop>().Where(x => x.RouteId == id).OrderBy(x => x.SortOrder).ToListAsync();
            return routeStops;
        }
    }
}
