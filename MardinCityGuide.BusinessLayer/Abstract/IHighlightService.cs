using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IHighlightService : IGenericService<Highlight>
    {
        Task<List<Highlight>> TGetHighlightListBySortOrderAsync();

    }
}
