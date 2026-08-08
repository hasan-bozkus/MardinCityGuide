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
        public BazaarManager(IGenericDal<Bazaar> genericDal) : base(genericDal)
        {
        }
    }
}
