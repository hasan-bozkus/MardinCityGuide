using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.Mobile.Dtos.PlaceDtos
{
    public class ResultGetPlaceByIdDto
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string CoverImageUrl { get; set; }
        public string Tag { get; set; }
        public string TypeLabel { get; set; }
        public string LocationName { get; set; }
        public string PlaceType { get; set; }
    }
}
