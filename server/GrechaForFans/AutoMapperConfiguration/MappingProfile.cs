using AutoMapper;
using DAL.Models;
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperConfiguration
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lot, LotDto>().ReverseMap();
            CreateMap<Price, PriceDto>().ReverseMap();
            CreateMap<Shop, ShopDto>().ReverseMap();
        }
    }
}
