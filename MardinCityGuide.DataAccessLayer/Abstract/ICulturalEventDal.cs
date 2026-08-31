using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface ICulturalEventDal : IGenericDal<CulturalEvent>
    {
        Task<List<CulturalEvent>> GetUpcoming2EventsAsync();

        Task<List<CulturalEvent>> GetUpcoming4EventsWithImageAsync();
        Task<List<CulturalEvent>> GetUpcoming4EventsWithAsync();
    }
}
