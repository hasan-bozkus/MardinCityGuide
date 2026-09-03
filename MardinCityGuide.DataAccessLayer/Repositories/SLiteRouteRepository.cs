using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
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

        public async Task<List<Route>> GetRouteListWithCategoryIsGastronomyAsync()
        {
            await _appDatabase.InitAsync();
            var routeCountWithCategoryGastronomy = await _connection.Table<Route>().Where(x => x.IsActive == true && x.Category == RouteCategory.Mutfak).CountAsync();
            var random = new Random().Next(0, routeCountWithCategoryGastronomy);

            var values = await _connection.Table<Route>().Where(x => x.IsActive == true && x.Category == RouteCategory.Mutfak).Skip(random - 1).Take(2).ToListAsync();
            return values;
        }

        public async Task<List<Route>> GetRouteListWithCategoryIsPhotographyAsync()
        {
            await _appDatabase.InitAsync();
            var routeCountWithCategoryPhotography = await _connection.Table<Route>().Where(x => x.IsActive == true && x.Category == RouteCategory.Fotoğrafçılık).CountAsync();
            var random = new Random().Next(0, routeCountWithCategoryPhotography);

            var values = await _connection.Table<Route>().Where(x => x.IsActive == true && x.Category == RouteCategory.Fotoğrafçılık).Skip(random - 1).Take(2).ToListAsync();

            var routeIds = values.Select(x => x.RouteId).ToList();
            var startedRouteStop = await _connection.Table<RouteStop>().Where(x => x.IsActive == true && routeIds.Contains(x.RouteId)).ToListAsync();

            foreach (var route in values)
            {
                var firstStop = startedRouteStop.FirstOrDefault(x => x.RouteId == route.RouteId);
                if (firstStop != null)
                {
                    route.BestTimeLabel = firstStop.BestTimeLabel;
                }
            }

            return values;
        }
    }
}
