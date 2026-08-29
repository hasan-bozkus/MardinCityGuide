using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface ISeasonalSpotService : IGenericService<SeasonalSpot>
    {
        Task<List<SeasonalSpot>> TGetRandom2ReligiousListAsync();
    }
}
