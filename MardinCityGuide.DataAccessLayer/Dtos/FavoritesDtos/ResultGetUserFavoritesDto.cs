using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Dtos.FavoritesDtos
{
    public class ResultGetUserFavoritesDto
    {
        public int FavoriteId { get; set; }
        public decimal Rating { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string CategoryTag { get; set; }
        public bool IsFavoriteColor { get; set; }
        public int TargetId { get; set; }
        public FavoriteTypeEnumDto FavoriteType { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
