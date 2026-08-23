using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
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

        public async Task<List<object>> TGetUserFavoritesAsync(int id, FavoriteType? filterType = null)
        {
            return await _favoriteDal.GetUserFavoritesAsync(id, filterType);
        }

        public async Task<bool> TRemoveFavoriteAsync(int id)
        {
            return await _favoriteDal.RemoveFavoriteAsync(id);
        }
    }
}
