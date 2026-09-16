using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Dtos.FavoritesDtos;
using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class FavoriteManager : GenericManager<Favorite>, IFavoriteService
    {
        private readonly IFavoriteDal _favoriteDal;

        public FavoriteManager(IGenericDal<Favorite> genericDal, IFavoriteDal favoriteDal) : base(genericDal)
        {
            _favoriteDal = favoriteDal;
        }

        public async Task<bool> TRemoveFavoriteAsync(int targetId, string favoriteType)
        {
            return await _favoriteDal.RemoveFavoriteAsync(targetId, favoriteType);
        }

        public async Task<Favorite> TGetFavoritePlaceByTargetIdRestaurantAsync(int id)
        {
            return await _favoriteDal.GetFavoritePlaceByTargetIdRestaurantAsync(id);
        }

        public async Task<List<ResultGetUserFavoritesDto>> TGetUserFavoritesAsync(int id, FavoriteType? filterType = null)
        {
            return await _favoriteDal.GetUserFavoritesAsync(id, filterType);
        }

        public async Task<bool> TAddFavoriteAsync(int targetId, string favoriteType)
        {
            return await _favoriteDal.AddFavoriteAsync(targetId, favoriteType);
        }

        public async Task<Favorite> TGetFavoriteReligiousSiteByTargetIdSiteAsync(int id)
        {
            return await _favoriteDal.GetFavoriteReligiousSiteByTargetIdSiteAsync(id);
        }
    }
}
