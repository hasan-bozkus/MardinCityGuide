using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class CulturalEventManager : GenericManager<CulturalEvent>, ICulturalEventService
    {
        private readonly ICulturalEventDal _culturalEventDal;

        public CulturalEventManager(IGenericDal<CulturalEvent> genericDal, ICulturalEventDal culturalEventDal) : base(genericDal)
        {
            _culturalEventDal = culturalEventDal;
        }

        public async Task<List<CulturalEvent>> TGetUpcoming2EventsAsync()
        {
            return await _culturalEventDal.GetUpcoming2EventsAsync();
        }

        public async Task<List<CulturalEvent>> TGetUpcoming4EventsWithAsync()
        {
            return await _culturalEventDal.GetUpcoming4EventsWithAsync();
        }

        public async Task<List<CulturalEvent>> TGetUpcoming4EventsWithImageAsync()
        {
            return await _culturalEventDal.GetUpcoming4EventsWithImageAsync();
        }
    }
}
