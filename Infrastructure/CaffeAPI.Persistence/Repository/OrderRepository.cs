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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrderWithDetailAsync()
        {
            var result = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .ThenInclude(x=>x.Category)
                .ToListAsync();

            return result;
        }
        public async Task<Order> GetOrderByIdWithDetailAsync(int orderId)
        {
            var result = await _context.Orders
                .Where(x => x.Id == orderId)
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
