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

            await _connection.CreateTableAsync<ArtisanCraft>();
            await _connection.CreateTableAsync<Bazaar>();
            await _connection.CreateTableAsync<Category>();
            await _connection.CreateTableAsync<CulturalEvent>();
            await _connection.CreateTableAsync<EditorialHighlight>();
            await _connection.CreateTableAsync<Favorite>();
            await _connection.CreateTableAsync<GalleryImage>();
            await _connection.CreateTableAsync<Highlight>();
            await _connection.CreateTableAsync<HistoricalSite>();
            await _connection.CreateTableAsync<HomeNavTile>();
            await _connection.CreateTableAsync<EntityLayer.Concrete.Location>();
            await _connection.CreateTableAsync<Museum>();
            await _connection.CreateTableAsync<Place>();
            await _connection.CreateTableAsync<ReligiousSite>();
            await _connection.CreateTableAsync<Route>();
            await _connection.CreateTableAsync<RouteStop>();
            await _connection.CreateTableAsync<SeasonalSpot>();
            await _connection.CreateTableAsync<Shop>();
            await _connection.CreateTableAsync<SkyGazingInfo>();
            await _connection.CreateTableAsync<User>();
            await _connection.CreateTableAsync<VisitInfo>();
            await _connection.CreateTableAsync<VisitorGuideTip>();

            _isInitialized = true;
        }
    }
}
