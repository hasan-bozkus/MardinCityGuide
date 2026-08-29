using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class SeasonalSpotManager : GenericManager<SeasonalSpot>, ISeasonalSpotService
    {
        private readonly ISeasonalSpotDal _seasonalSpotDal;

        public SeasonalSpotManager(IGenericDal<SeasonalSpot> genericDal, ISeasonalSpotDal seasonalSpotDal) : base(genericDal)
        {
            _seasonalSpotDal = seasonalSpotDal;
        }

        public async Task<List<SeasonalSpot>> TGetRandom2ReligiousListAsync()
        {
            return await _seasonalSpotDal.GetRandom2ReligiousListAsync();
        }
    }
}
