using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Concrete;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using SQLite;

namespace MardinCityGuide.DataAccessLayer.Repositories
{
    public class SLiteFavoriteRepository : GenericRepository<Favorite>, IFavoriteDal
    {
        private readonly AppDatabase _appDatabase;
        private readonly SQLiteAsyncConnection _connection;

        public SLiteFavoriteRepository(AppDatabase appDatabase, SQLiteAsyncConnection connection) : base(appDatabase, connection)
        {
            _appDatabase = appDatabase;
            _connection = connection;
        }

        public async Task<List<object>> GetUserFavoritesAsync(int id, FavoriteType? filterType = null)
        {
            await _appDatabase.InitAsync();

            var query = _connection.Table<Favorite>().Where(f => f.UserId == id && f.IsActive == true);

            if(filterType.HasValue)
            {
                query = query.Where(f => f.FavoriteType == filterType.Value);
            }

            var favorites = await query.ToListAsync();
            var resultList = new List<object>();

            foreach(var item in favorites)
            {
                switch(item.FavoriteType)
                {
                    case FavoriteType.Restaurant:
                        var rest = await _connection.Table<Place>().FirstOrDefaultAsync(r => r.PlaceId == item.TargetId);
                        var location = await _connection.Table<EntityLayer.Concrete.Location>().Where(x => x.LocationId == rest.LocationId).FirstOrDefaultAsync();

                        if(rest != null)
                        {
                            resultList.Add(new
                            {
                                FavoriteId = item.FavoriteId,
                                TargetId = item.TargetId,
                                FavoriteType = item.FavoriteType,
                                Title = rest.Name,
                                Location = location.AddressText,
                                CategoryTag = rest.TypeLabel,
                                Rating = rest.Rating,
                                ImageUrl = rest.CoverImageUrl,
                                CreatedAt = item.CreatedAt
                            });
                        }
                        break;

                    case FavoriteType.Site:
                        var religiousSite = await _connection.Table<ReligiousSite>().FirstOrDefaultAsync(r => r.ReligiousSiteId == item.TargetId);
                        var siteLocation = await _connection.Table<EntityLayer.Concrete.Location>().Where(x => x.LocationId == religiousSite.LocationId).FirstOrDefaultAsync();
                        if (religiousSite != null)
                        {
                            resultList.Add(new
                            {
                                FavoriteId = item.FavoriteId,
                                TargetId = item.TargetId,
                                FavoriteType = item.FavoriteType,
                                Title = religiousSite.Name,
                                Location = siteLocation.AddressText,
                                CategoryTag = religiousSite.SiteType,
                                Rating = religiousSite.Rating,
                                ImageUrl = religiousSite.ImageUrl,
                                CreatedAt = item.CreatedAt
                            });
                        }
                        break;

                    case FavoriteType.Route: // Gezi Rotaları
                        var route = await _connection.Table<Route>().FirstOrDefaultAsync(r => r.RouteId == item.TargetId);
                        if (route != null)
                        {
                            resultList.Add(new 
                            {
                                FavoriteId = item.FavoriteId,
                                TargetId = item.TargetId,
                                FavoriteType = item.FavoriteType,
                                Title = route.Name,
                                Location = route.StopCountLabelOverride,
                                CategoryTag = route.Category,
                                Rating = route.Rating,
                                ImageUrl = route.CoverImageUrl,
                                CreatedAt = item.CreatedAt
                            });
                        }
                        break;
                }
            }

            return resultList.ToList(); //content page alanıda orderbydescending ile CreatedAt baz alınarak listeleme yapılacak
        }

        public Task<bool> RemoveFavoriteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
