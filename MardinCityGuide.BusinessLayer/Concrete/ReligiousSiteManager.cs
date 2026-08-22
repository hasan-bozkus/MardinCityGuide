using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class ReligiousSiteManager : GenericManager<ReligiousSite>, IReligiousSiteService
    {
        private readonly IReligiousSiteDal _religiousSiteDal;

        public ReligiousSiteManager(IGenericDal<ReligiousSite> genericDal, IReligiousSiteDal religiousSiteDal) : base(genericDal)
        {
            _religiousSiteDal = religiousSiteDal;
        }

        public async Task<List<ReligiousSite>> TGetMosquesAndMonasteriesListAsync()
        {
            return await _religiousSiteDal.GetMosquesAndMonasteriesListAsync();
        }
    }
}
