using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<List<ResultMenuItemDto>> GetAllMenuItems();
        Task<DetailMenuItemDto> GetByIdMenuItem(int id);
        Task AddMenuItem(CreateMenuItemDto dto);
        Task UpdateMenuItem(UpdateMenuItemDto dto);
        Task DeleteMenuItem(int id);
    }
}
