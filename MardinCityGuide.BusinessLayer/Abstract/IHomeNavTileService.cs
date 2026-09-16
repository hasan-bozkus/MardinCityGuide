using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IHomeNavTileService : IGenericService<HomeNavTile>
    {
        Task<List<HomeNavTile>> TGetHomeNavTileListBySortOrderAndIsActiveAsync();

        Task<HomeNavTile> TGetHomeNawTitleByTitleAsync(string title);
    }
}
