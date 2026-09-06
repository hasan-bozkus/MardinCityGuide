using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class PlaceManager : GenericManager<Place>, IPlaceService
    {
        private readonly IPlaceDal _placeDal;

        public PlaceManager(IGenericDal<Place> genericDal, IPlaceDal placeDal) : base(genericDal)
        {
            _placeDal = placeDal;
        }

        public async Task TGetChangeIsFavoriteStatusFalseAsync(int id)
        {
            await _placeDal.GetChangeIsFavoriteStatusFalseAsync(id);
        }

        public async Task TGetChangeIsFavoriteStatusTrueAsync(int id)
        {
            await _placeDal.GetChangeIsFavoriteStatusTrueAsync(id);
        }

        public async Task<Place> TGetHighestStarredPlaceWithLocationAsync()
        {
            return await _placeDal.GetHighestStarredPlaceWithLocationAsync();
        }

        public async Task<List<Place>> TGetPlaceListWithLocationByIsActiveAndIsFeatuderAsync()
        {
            return await _placeDal.GetPlaceListWithLocationByIsActiveAndIsFeatuderAsync();
        }

        public async Task<List<Place>> TGetPlaceListWithSortOrderAsync()
        {
            return await _placeDal.GetPlaceListWithSortOrderAsync();
        }

        public async Task<Place> TGetPlaceWithLocationAndCategoryAsync(int id)
        {
            return await _placeDal.GetPlaceWithLocationAndCategoryAsync(id);
        }

        public async Task<Place> TGetRandomPlaceAsync()
        {
            return await _placeDal.GetRandomPlaceAsync();
        }
    }
}
