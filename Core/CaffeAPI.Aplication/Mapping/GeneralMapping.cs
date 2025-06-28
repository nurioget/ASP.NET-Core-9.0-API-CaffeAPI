using AutoMapper;
using CaffeAPI.Aplication.Dtos.CategoryDtos;
using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using CaffeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category,ResultCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryDto>().ReverseMap();
            CreateMap<Category,DetailCategoryDto>().ReverseMap();

            CreateMap<MenuItem,ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem,CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem,UpdateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem,DetailMenuItemDto>().ReverseMap();
        }
    }
}
