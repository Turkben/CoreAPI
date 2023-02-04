using AutoMapper;
using CoreAPI.Core.DTOs;
using CoreAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Service.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductDto,Product>().ReverseMap();
            CreateMap<UserDto,User>().ReverseMap();
        }
    }
}
