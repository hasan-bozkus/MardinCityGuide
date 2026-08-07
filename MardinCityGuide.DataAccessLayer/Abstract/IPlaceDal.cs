using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IPlaceDal : IGenericDal<Place>
    {
        Task<List<Place>> GetPlaceListWithSortOrderAsync();

        Task<Place> GetRandomPlaceAsync();

        Task<Place> GetPlaceWithLocationAndCategoryAsync(int id);
    }
}
