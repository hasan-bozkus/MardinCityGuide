using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface ICulturalEventService : IGenericService<CulturalEvent>
    {
        Task<List<CulturalEvent>> TGetUpcoming2EventsAsync();
        Task<List<CulturalEvent>> TGetUpcoming4EventsWithImageAsync();
        Task<List<CulturalEvent>> TGetUpcoming4EventsWithAsync();
    }
}
