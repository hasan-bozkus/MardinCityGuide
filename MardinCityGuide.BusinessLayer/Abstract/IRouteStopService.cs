using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IRouteStopService : IGenericService<RouteStop>
    {
        Task<List<RouteStop>> TGetRouteStopsByRouteIdAsync(int id);
    }
}
