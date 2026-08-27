using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface ISkyGazingInfoService : IGenericService<SkyGazingInfo>
    {
        Task<SkyGazingInfo> TLoadSkyGazingDataForMardinAsync();
    }
}
