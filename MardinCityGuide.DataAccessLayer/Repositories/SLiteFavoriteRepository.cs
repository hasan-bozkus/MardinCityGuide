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

        public async Task<bool> AddFavoriteAsync(int targetId, string favoriteType)
        {
            await _appDatabase.InitAsync();

            var favoriteTypeValue = await _connection.Table<Favorite>().Where(x => x.TargetId == targetId && x.FavoriteType == (FavoriteType)Enum.Parse(typeof(FavoriteType), favoriteType)).FirstOrDefaultAsync();

            if (favoriteTypeValue.FavoriteType == FavoriteType.Site)
            {
                var site = await _connection.Table<ReligiousSite>().Where(s => s.ReligiousSiteId == targetId).FirstOrDefaultAsync();
                site.IsFavorite = true;
                await _connection.UpdateAsync(site);
            }

            if (favoriteTypeValue.FavoriteType == FavoriteType.Restaurant)
            {
                var site = await _connection.Table<Place>().Where(s => s.PlaceId == targetId).FirstOrDefaultAsync();
                site.IsFavorite = true;
                await _connection.UpdateAsync(site);
            }

            if (favoriteTypeValue.FavoriteType == FavoriteType.Route)
            {
                var site = await _connection.Table<Route>().Where(s => s.RouteId == targetId).FirstOrDefaultAsync();
                site.IsActive = true;
                await _connection.UpdateAsync(site);
            }

            return true;
        }

        public async Task<Favorite> GetFavoritePlaceByTargetIdRestaurantAsync(int id)
        {
            var value = await _connection.Table<Favorite>().Where(x => x.TargetId == id && x.FavoriteType == FavoriteType.Restaurant).FirstOrDefaultAsync();
            return value;
        }

        public async Task<List<object>> GetUserFavoritesAsync(int id, FavoriteType? filterType = null)
        {
            await _appDatabase.InitAsync();

            var query = _connection.Table<Favorite>().Where(f => f.UserId == id && f.IsActive == true);

            if (filterType.HasValue)
            {
                query = query.Where(f => f.FavoriteType == filterType.Value);
            }

            var favorites = await query.ToListAsync();
            var resultList = new List<object>();

            foreach (var item in favorites)
            {
                switch (item.FavoriteType)
                {
                    case FavoriteType.Restaurant:
                        var rest = await _connection.Table<Place>().FirstOrDefaultAsync(r => r.PlaceId == item.TargetId);
                        var location = await _connection.Table<EntityLayer.Concrete.Location>().Where(x => x.LocationId == rest.LocationId).FirstOrDefaultAsync();

                        if (rest != null)
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
                    default:
                        var targetid = await _connection.Table<ReligiousSite>().FirstOrDefaultAsync();
                        var targetLocation = await _connection.Table<EntityLayer.Concrete.Location>().Where(x => x.LocationId == targetid.LocationId).FirstOrDefaultAsync();
                        resultList.Add(new
                        {
                            FavoriteId = item.FavoriteId,
                            TargetId = item.TargetId,
                            FavoriteType = item.FavoriteType,
                            Title = targetid.Name,
                            Location = targetLocation.AddressText,
                            CategoryTag = targetid.Dynasty,
                            Rating = targetid.Rating,
                            ImageUrl = targetid.ImageUrl,
                            CreatedAt = item.CreatedAt
                        });
                        break;

                }
            }

            return resultList.Cast<dynamic>().OrderByDescending(f => f.CreatedAt).ToList();
        }

        public async Task<bool> RemoveFavoriteAsync(int targetId, string favoriteType)
        {
            await _appDatabase.InitAsync();
            var favorite = await _connection.Table<Favorite>().Where(f => f.TargetId == targetId).FirstOrDefaultAsync();

            if (favoriteType == FavoriteType.Restaurant.ToString())
            {
                var restaurant = await _connection.Table<Place>().Where(s => s.PlaceId == favorite.TargetId).FirstOrDefaultAsync();
                if (restaurant != null)
                {
                    restaurant.IsFavorite = false;
                    await _connection.UpdateAsync(restaurant);
                }
            }
            if (favoriteType == FavoriteType.Site.ToString())
            {
                var site = await _connection.Table<ReligiousSite>().Where(s => s.ReligiousSiteId == favorite.TargetId).FirstOrDefaultAsync();
                if (site != null)
                {
                    site.IsFavorite = false;
                    await _connection.UpdateAsync(site);
                }
            }
            if (favoriteType == FavoriteType.Route.ToString())
            {
                var route = await _connection.Table<Route>().Where(s => s.RouteId == favorite.TargetId).FirstOrDefaultAsync();
                if (route != null)
                {
                    route.IsActive = false;
                    await _connection.UpdateAsync(route);
                }
            }
            await _connection.DeleteAsync(favorite);
            return true;
        }
    }
}
