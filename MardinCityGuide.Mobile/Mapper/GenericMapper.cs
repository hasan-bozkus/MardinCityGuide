using AutoMapper;
using MardinCityGuide.DataAccessLayer.Dtos.FavoritesDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.Mobile.Mapper
{
    public class GenericMapper : Profile
    {
        public GenericMapper()
        {
            CreateMap<object, ResultGetUserFavoritesDto>().ReverseMap();
        }
    }
}
