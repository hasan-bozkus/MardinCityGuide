using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Abstract
{
    public interface IEditorialHighlightService : IGenericService<EditorialHighlight>
    {
        Task<EditorialHighlight> TGetRandomEditoralHighlightAsync();

    }
}
