using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using URLShortner.Dtos;
namespace URLShortner
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrignalUrlInput, UrlInfo>();
            CreateMap<UrlInfo, OrignalUrlResponse>();
            CreateMap<ShortenUrlInput, UrlInfo>();
            CreateMap<UrlInfo, ShortenUrlResponse>();
        }
    }
}
