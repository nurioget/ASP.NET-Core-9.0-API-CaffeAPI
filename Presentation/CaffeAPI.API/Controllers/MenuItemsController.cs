﻿using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : BaseController
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemsController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var result = await _menuItemServices.GetAllMenuItems();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _menuItemServices.GetByIdMenuItem(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem(CreateMenuItemDto dto)
        {
            var result = await _menuItemServices.AddMenuItem(dto);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            var result = await _menuItemServices.UpdateMenuItem(dto);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _menuItemServices.DeleteMenuItem(id);
            return CreateResponse(result);
        }
    }
}