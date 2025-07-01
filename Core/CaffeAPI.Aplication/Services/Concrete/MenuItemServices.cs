using AutoMapper;
using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Services.Abstract;
using CaffeAPI.Domain.Entities;
using FluentValidation;
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
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemValidator, IGenericRepository<Category> categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
            _createMenuItemValidator = createMenuItemValidator;
            _updateMenuItemValidator = updateMenuItemValidator;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto)
        {
            try
            {
                var validate = await _createMenuItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes = ErrorCodes.ValidationError };
                }
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory==null)
                {
                    return new ResponseDto<object> { Success = false, Data = dto, Message = "Eklmek istediğiniz kategori bulunamadı.", ErrorCodes = ErrorCodes.NotFound };
                }
                var menuItem = _mapper.Map<MenuItem>(dto);
                await _menuItemRepository.AddAsync(menuItem);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Menu Item Başarıyla Eklendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu: " + ex.Message, ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if (menuItem==null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Menu Item Bulunamadı" ,ErrorCodes=ErrorCodes.NotFound};
                }
                await _menuItemRepository.DeleteAsync(menuItem);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Menu Item Başarıyla Silindi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu: " + ex.Message, ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                var category=await _categoryRepository.GetAllAsync();
                if (menuItems.Count == 0)
                {
                    return new ResponseDto<List<ResultMenuItemDto>> { Success = false, Data = null, Message = "Menü Items Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultMenuItemDto>> { Success = false, Data = null, Message = "Bir hata oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            var category=await _categoryRepository.GetByIdAsync(menuItem.CategoryId);   
            if (menuItem == null)
            {
                return new ResponseDto<DetailMenuItemDto> { Success = false, Data = null, Message = "Menu Item bulunamadı", ErrorCodes = ErrorCodes.NotFound };
            }
            var result = _mapper.Map<DetailMenuItemDto>(menuItem);
            return new ResponseDto<DetailMenuItemDto> { Success = true, Data = result };
        }

        public async Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            try
            {
                var validate = await _updateMenuItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes = ErrorCodes.ValidationError };
                }
                var menuItem = await _menuItemRepository.GetByIdAsync(dto.Id);
                if (menuItem == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Menu Item Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object> { Success = false, Data = dto, Message = "Eklmek istediğiniz kategori bulunamadı.", ErrorCodes = ErrorCodes.NotFound };
                }
                var newMenuItem = _mapper.Map(dto, menuItem);
                await _menuItemRepository.UpdateAsync(newMenuItem);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Menu Item Başarıyla Güncellendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu: " + ex.Message, ErrorCodes = ErrorCodes.Exception };
            }
        }
    }
}
