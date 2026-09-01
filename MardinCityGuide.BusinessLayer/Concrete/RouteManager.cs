using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class RouteManager : GenericManager<Route>, IRouteService
    {
        private readonly IRouteDal _routeDal;

        public RouteManager(IGenericDal<Route> genericDal, IRouteDal routeDal) : base(genericDal)
        {
            _routeDal = routeDal;
        }

        public async Task<Route> TGetOneRandomRouteAsync()
        {
            return await _routeDal.GetOneRandomRouteAsync();
        }
    }
}
