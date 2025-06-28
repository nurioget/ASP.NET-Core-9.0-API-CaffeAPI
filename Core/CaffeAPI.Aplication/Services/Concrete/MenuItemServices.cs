using AutoMapper;
using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Services.Abstract;
using CaffeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task AddMenuItem(CreateMenuItemDto dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _menuItemRepository.AddAsync(menuItem);
        }

        public async Task DeleteMenuItem(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            await _menuItemRepository.DeleteAsync(menuItem);
        }

        public async Task<List<ResultMenuItemDto>> GetAllMenuItems()
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
            return result;
        }

        public async Task<DetailMenuItemDto> GetByIdMenuItem(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            var result = _mapper.Map<DetailMenuItemDto>(menuItem);
            return result;
        }

        public async Task UpdateMenuItem(UpdateMenuItemDto dto)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(dto.Id);
            var newMenuItem = _mapper.Map(dto, menuItem);
            await _menuItemRepository.UpdateAsync(newMenuItem);
        }
    }
}
