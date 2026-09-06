using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IFavoriteDal : IGenericDal<Favorite>
    {
        Task<List<object>> GetUserFavoritesAsync(int id, FavoriteType? filterType = null);
        Task<bool> RemoveFavoriteAsync(int id);

        Task<Favorite> GetFavoritePlaceByTargetIdRestaurantAsync(int id);
    }
}
