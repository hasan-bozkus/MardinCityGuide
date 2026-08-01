using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Abstract
{
    public interface IHighlightDal : IGenericDal<Highlight>
    {
        Task<List<Highlight>> GetHighlightListBySortOrderAsync();
    }
}
