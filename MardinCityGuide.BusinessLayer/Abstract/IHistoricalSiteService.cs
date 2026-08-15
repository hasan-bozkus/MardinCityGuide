using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IHistoricalSiteService : IGenericService<HistoricalSite>
    {
        Task<HistoricalSite> TGetFirstHistoricalSiteWithImageGaleriesAsync();
    }
}
