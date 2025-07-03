﻿using CaffeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Interfaces
{
    public interface ITableRepository
    {
        Task<Table> GetByTableNumberAsync(int tableNumber);
        Task<List<Table>> GetAllActiveTablesAsync();
    }
}
