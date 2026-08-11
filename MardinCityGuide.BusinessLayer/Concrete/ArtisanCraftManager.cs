using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class ArtisanCraftManager : GenericManager<ArtisanCraft>, IArtisanCraftService
    {
        private readonly IArtisanCraftDal _artisanCraftDal;

        public ArtisanCraftManager(IGenericDal<ArtisanCraft> genericDal, IArtisanCraftDal artisanCraftDal) : base(genericDal)
        {
            _artisanCraftDal = artisanCraftDal;
        }

        public async Task<List<ArtisanCraft>> TGetIsActiveArtisanCraftListAsync()
        {
            return await _artisanCraftDal.GetIsActiveArtisanCraftListAsync();
        }

        public async Task<List<ArtisanCraft>> TGetRandom2ArtisanCraftWithCategoryAsycn()
        {
            return await _artisanCraftDal.GetRandom2ArtisanCraftWithCategoryAsycn();
        }
    }
}
