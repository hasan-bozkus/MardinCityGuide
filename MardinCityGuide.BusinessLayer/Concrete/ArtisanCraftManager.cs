using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class ArtisanCraftManager : GenericManager<ArtisanCraft>, IArtisanCraftService
    {
        public ArtisanCraftManager(IGenericDal<ArtisanCraft> genericDal) : base(genericDal)
        {
        }
    }
}
