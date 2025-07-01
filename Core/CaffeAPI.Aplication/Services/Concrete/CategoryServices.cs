﻿using AutoMapper;
using CaffeAPI.Aplication.Dtos.CategoryDtos;
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
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto)
        {
            try
            {
                var validate = await _createCategoryValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Message = string.Join(", ", validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes = ErrorCodes.ValidationError };
                }
                var category = _mapper.Map<Category>(dto);
                await _categoryRepository.AddAsync(category);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kategori Oluşturuldu" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata oluştu", ErrorCodes = ErrorCodes.Exception };
            }

        }

        public async Task<ResponseDto<object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                await _categoryRepository.DeleteAsync(category);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kategori Silindi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
            }

        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>> { Success = false, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultCategoryDto>>(categories);
                return new ResponseDto<List<ResultCategoryDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCategoryDto>> { Success = false, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
            }

        }

        public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<DetailCategoryDto> { Success = false, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailCategoryDto>(category);
                return new ResponseDto<DetailCategoryDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailCategoryDto> { Success = false, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
            }

        }

        public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var validate = await _updateCategoryValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(", ", validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes = ErrorCodes.ValidationError };
                }
                var categorydb = await _categoryRepository.GetByIdAsync(dto.Id);
                if (categorydb == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var category = _mapper.Map(dto, categorydb);
                await _categoryRepository.UpdateAsync(category);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kategori Güncellendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }
    }
}
