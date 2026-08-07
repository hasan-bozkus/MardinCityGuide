using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IPlaceService : IGenericService<Place>
    {
        Task<List<Place>> TGetPlaceListWithSortOrderAsync();
        Task<Place> TGetRandomPlaceAsync();

        Task<Place> TGetPlaceWithLocationAndCategoryAsync(int id);
    }
}
