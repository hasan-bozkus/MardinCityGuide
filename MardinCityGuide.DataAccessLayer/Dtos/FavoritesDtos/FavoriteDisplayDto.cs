using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.DataAccessLayer.Dtos.FavoritesDtos
{
    public class FavoriteDisplayDto
    {
        public int FavoriteId { get; set; }
        public int TargetId { get; set; }
        public FavoriteTypeEnumDto FavoriteType { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SubtitleOrLocation { get; set; } = string.Empty;
        public string CategoryTag { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
