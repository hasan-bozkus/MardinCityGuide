using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class MuseumManager : GenericManager<Museum>, IMuseumService
    {
        private readonly IMuseumDal _museumDal;

        public MuseumManager(IGenericDal<Museum> genericDal, IMuseumDal museumDal) : base(genericDal)
        {
            _museumDal = museumDal;
        }
    }
}
