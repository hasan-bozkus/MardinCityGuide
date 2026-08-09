using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IBazaarDal : IGenericDal<Bazaar>
    {
        Task<List<Bazaar>> GetBazaarsWithCategoryAsync();
    }
}
