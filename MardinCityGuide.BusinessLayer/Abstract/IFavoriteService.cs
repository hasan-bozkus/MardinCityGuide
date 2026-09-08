using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IFavoriteService : IGenericService<Favorite>
    {
        Task<List<object>> TGetUserFavoritesAsync(int id, FavoriteType? filterType = null);
        Task<bool> TRemoveFavoriteAsync(int targetId, string favoriteType);
        Task<Favorite> TGetFavoritePlaceByTargetIdRestaurantAsync(int id);
        Task<bool> TAddFavoriteAsync(int targetId, string favoriteType);

    }
}
