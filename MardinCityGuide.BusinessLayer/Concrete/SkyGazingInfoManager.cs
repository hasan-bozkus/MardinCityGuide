using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class SkyGazingInfoManager : GenericManager<SkyGazingInfo>, ISkyGazingInfoService
    {
        private readonly ISkyGazingInfoDal _skyGazingInfoDal;
        public SkyGazingInfoManager(IGenericDal<SkyGazingInfo> genericDal, ISkyGazingInfoDal skyGazingInfoDal) : base(genericDal)
        {
            _skyGazingInfoDal = skyGazingInfoDal;
        }

        public async Task<SkyGazingInfo> TLoadSkyGazingDataForMardinAsync()
        {
            return await _skyGazingInfoDal.LoadSkyGazingDataForMardinAsync();
        }
    }
}
