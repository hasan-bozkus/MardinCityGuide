using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class HomeNavTileManager : GenericManager<HomeNavTile>, IHomeNavTileService
    {
        private readonly IHomeNavTileDal _homeNavTileDal;

        public HomeNavTileManager(IGenericDal<HomeNavTile> genericDal, IHomeNavTileDal homeNavTileDal) : base(genericDal)
        {
            _homeNavTileDal = homeNavTileDal;
        }

        public async Task<List<HomeNavTile>> TGetHomeNavTileListBySortOrderAndIsActiveAsync()
        {

            return await _homeNavTileDal.GetHomeNavTileListBySortOrderAndIsActiveAsync();
        }
    }
}
