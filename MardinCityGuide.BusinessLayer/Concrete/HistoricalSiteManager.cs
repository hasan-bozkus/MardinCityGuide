using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class HistoricalSiteManager : GenericManager<HistoricalSite>, IHistoricalSiteService
    {
        private readonly IHistoricalSiteDal _historicalSiteDal;

        public HistoricalSiteManager(IGenericDal<HistoricalSite> genericDal, IHistoricalSiteDal historicalSiteDal) : base(genericDal)
        {
            _historicalSiteDal = historicalSiteDal;
        }

        public async Task<HistoricalSite> TGetFirstHistoricalSiteWithImageGaleriesAsync()
        {
            return await _historicalSiteDal.GetFirstHistoricalSiteWithImageGaleriesAsync();
        }
    }
}
