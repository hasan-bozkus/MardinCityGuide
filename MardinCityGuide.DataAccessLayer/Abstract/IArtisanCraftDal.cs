using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IArtisanCraftDal : IGenericDal<ArtisanCraft>
    {
        Task<List<ArtisanCraft>> GetRandom2ArtisanCraftWithCategoryAsycn();
        Task<List<ArtisanCraft>> GetIsActiveArtisanCraftListAsync();

    }
}
