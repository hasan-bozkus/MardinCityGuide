using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IArtisanCraftService : IGenericService<ArtisanCraft>
    {
        Task<List<ArtisanCraft>> TGetRandom2ArtisanCraftWithCategoryAsycn();
        Task<List<ArtisanCraft>> TGetIsActiveArtisanCraftListAsync();

    }
}
