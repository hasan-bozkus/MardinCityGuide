using MardinCityGuide.BusinessLayer.Abstract;
using MardinCityGuide.DataAccessLayer.Abstract;
using MardinCityGuide.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.BusinessLayer.Concrete
{
    public class EditorialHighlightManager : GenericManager<EditorialHighlight>, IEditorialHighlightService
    {
        private readonly IEditorialHighlightDal _editoralHighlightDal;

        public EditorialHighlightManager(IGenericDal<EditorialHighlight> genericDal, IEditorialHighlightDal editoralHighlightDal) : base(genericDal)
        {
            _editoralHighlightDal = editoralHighlightDal;
        }

        public async Task<EditorialHighlight> TGetRandomEditoralHighlightAsync()
        {
            return await _editoralHighlightDal.GetRandomEditoralHighlightAsync();
        }
    }
}
