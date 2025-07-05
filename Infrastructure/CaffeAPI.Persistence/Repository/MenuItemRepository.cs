using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Domain.Entities;
using CaffeAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Persistence.Repository
{
    public class MenuItemRepository: IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItemsFilterByCategoryId(int categoryId)
        {
            var result=await _context.MenuItems
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();

            return result;
        }
    }
}
