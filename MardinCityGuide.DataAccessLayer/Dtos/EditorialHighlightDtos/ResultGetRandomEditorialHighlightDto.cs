using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Dtos.EditorialHighlightDtos
{
    public class ResultGetRandomEditorialHighlightDto
    {
        public string BadgeLabel { get; set; }
        public string IllustrationUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CtaUrl { get; set; }
    }
}
