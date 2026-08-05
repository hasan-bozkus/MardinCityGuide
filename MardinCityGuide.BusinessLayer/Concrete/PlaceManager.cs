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

        public async Task<List<Place>> TGetPlaceListWithSortOrderAsync()
        {
            return await _placeDal.GetPlaceListWithSortOrderAsync();
        }

        public async Task<Place> TGetRandomPlaceAsync()
        {
            return await _placeDal.GetRandomPlaceAsync();
        }
    }
}
