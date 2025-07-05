﻿using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Dtos.CategoryDtos
{
    public class ResultCategoriesWithMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoriesMenuItemDto> MenuItems { get; set; }
    }
}
