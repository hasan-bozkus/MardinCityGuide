using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IReligiousSiteDal : IGenericDal<ReligiousSite>
    {
        Task<List<ReligiousSite>> GetMosquesAndMonasteriesListAsync();
        Task GetChangeIsFavoriteStatusTrueAsync(int id);
        Task GetChangeIsFavoriteStatusFalseAsync(int id);
    }
}
