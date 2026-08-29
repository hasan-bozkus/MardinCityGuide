using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface ISeasonalSpotDal : IGenericDal<SeasonalSpot>
    {
        Task<List<SeasonalSpot>> GetRandom2ReligiousListAsync();
    }
}
