using MardinCityGuide.EntityLayer.Concrete;
using MardinCityGuide.BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using MardinCityGuide.DataAccessLayer.Abstract;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        public CategoryManager(IGenericDal<Category> genericDal) : base(genericDal)
        {
        }
    }
}
