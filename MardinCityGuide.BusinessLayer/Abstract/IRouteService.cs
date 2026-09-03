using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IRouteService : IGenericService<Route>
    {
        Task<Route> TGetOneRandomRouteAsync();
        Task<List<Route>> TGetRouteListWithCategoryIsGastronomyAsync();
        Task<List<Route>> TGetRouteListWithCategoryIsPhotographyAsync();
    }
}
