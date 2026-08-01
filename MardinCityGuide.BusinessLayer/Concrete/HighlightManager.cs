using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class HighlightManager : GenericManager<Highlight>, IHighlightService
    {
        private readonly IHighlightDal _highlightDal;

        public HighlightManager(IGenericDal<Highlight> genericDal, IHighlightDal highlightDal) : base(genericDal)
        {
            _highlightDal = highlightDal;
        }

        public async Task<List<Highlight>> TGetHighlightListBySortOrderAsync()
        {
            return await _highlightDal.GetHighlightListBySortOrderAsync();
        }
    }
}
