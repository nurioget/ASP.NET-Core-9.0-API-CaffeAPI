using AutoMapper;
using CaffeAPI.Aplication.Dtos.CaffeInfoDtos;
using CaffeAPI.Aplication.Dtos.CategoryDtos;
using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using CaffeAPI.Aplication.Dtos.OrderDtos;
using CaffeAPI.Aplication.Dtos.OrderItemDtos;
using CaffeAPI.Aplication.Dtos.ReviewDtos;
using CaffeAPI.Aplication.Dtos.TablesDtos;
using CaffeAPI.Aplication.Dtos.UserDtos;
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
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, DetailCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoriesWithMenuDto>().ReverseMap();

            CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CategoriesMenuItemDto>().ReverseMap();

            CreateMap<Table, ResultTableDto>().ReverseMap();
            CreateMap<Table, CreateTableDto>().ReverseMap();
            CreateMap<Table, UpdateTableDto>().ReverseMap();
            CreateMap<Table, DetailTableDto>().ReverseMap();

            CreateMap<OrderItem, ResultOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, DetailOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();

            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, DetailOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();

            CreateMap<CaffeInfo, ResultCaffeInfoDto>().ReverseMap();
            CreateMap<CaffeInfo, DetailCaffeInfoDto>().ReverseMap();
            CreateMap<CaffeInfo, CreateCaffeInfoDto>().ReverseMap();
            CreateMap<CaffeInfo, UpdateCaffeInfoDto>().ReverseMap();

            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, DetailReviewDto>().ReverseMap();

        }
    }
}
