using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class RouteStopManager : GenericManager<RouteStop>, IRouteStopService
    {
        private readonly IRouteStopDal _routeStopDal;

        public RouteStopManager(IGenericDal<RouteStop> genericDal, IRouteStopDal routeStopDal) : base(genericDal)
        {
            _routeStopDal = routeStopDal;
        }

        public async Task<List<RouteStop>> TGetRouteStopsByRouteIdAsync(int id)
        {
            return await _routeStopDal.GetRouteStopsByRouteIdAsync(id);
        }
    }
}
