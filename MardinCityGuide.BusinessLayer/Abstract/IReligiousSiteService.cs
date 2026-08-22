using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IReligiousSiteService : IGenericService<ReligiousSite>
    {
        Task<List<ReligiousSite>> TGetMosquesAndMonasteriesListAsync();
    }
}
