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
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _context;

        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Table>> GetAllActiveTablesAsync()
        {
            var result = await _context.Tables
                .Where(x => x.IsActive == true)
                .ToListAsync();
            return result;
        }

        public async Task<Table> GetByTableNumberAsync(int tableNumber)
        {
            var result = await _context.Tables
                .FirstOrDefaultAsync(x => x.TableNumber == tableNumber);
            return result;
        }
    }
}
