using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class BazaarManager : GenericManager<Bazaar>, IBazaarService
    {
        private readonly IBazaarDal _bazaarDal;

        public BazaarManager(IGenericDal<Bazaar> genericDal, IBazaarDal bazaarDal) : base(genericDal)
        {
            _bazaarDal = bazaarDal;
        }

        public async Task<List<Bazaar>> TGetBazaarsWithCategoryAsync()
        {
            return await _bazaarDal.GetBazaarsWithCategoryAsync();
        }
    }
}
