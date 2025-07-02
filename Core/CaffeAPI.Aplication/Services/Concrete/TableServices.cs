﻿using AutoMapper;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Dtos.TablesDtos;
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
    public class TableServices : ITableServices
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTableDto> _createTableValidator;
        private readonly IValidator<UpdateTableDto> _updateTableValidator;
        private readonly ITableRepository _tableRepository1;

        public TableServices(IGenericRepository<Table> tableRepository, IMapper mapper, IValidator<CreateTableDto> createTableValidator, IValidator<UpdateTableDto> updateTableValidator, ITableRepository tableRepository1)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _createTableValidator = createTableValidator;
            _updateTableValidator = updateTableValidator;
            _tableRepository1 = tableRepository1;
        }

        public async Task<ResponseDto<object>> AddTable(CreateTableDto dto)
        {
            try
            {
                var validate = await _createTableValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes = ErrorCodes.ValidationError };
                }
                var checkTable = await _tableRepository1.GetByTableNumberAsync(dto.TableNumber);
                if (checkTable != null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Bu Masa Numarası Zaten Kullanılıyor", ErrorCodes = ErrorCodes.DuplicateError };
                }
                var result = _mapper.Map<Table>(dto);
                await _tableRepository.AddAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarıyla Eklendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> DeleteTable(int id)
        {
            try
            {
                var rp = await _tableRepository.GetByIdAsync(id);
                if (rp == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Masa Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                await _tableRepository.DeleteAsync(rp);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarıyla Silindi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
        {
            try
            {
                var rp = await _tableRepository.GetAllAsync();
                if (rp.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Masalar Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultTableDto>>(rp);
                return new ResponseDto<List<ResultTableDto>> { Success = true, Data = result };

            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber)
        {
            try
            {
                var table = await _tableRepository1.GetByTableNumberAsync(tableNumber);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Masalar Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto> { Success = true, Data = result };
            }
            catch (Exception)
            {
                return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetTableById(int id)
        {
            try
            {
                var rp = await _tableRepository.GetByIdAsync(id);
                if (rp == null)
                {
                    return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Masa Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailTableDto>(rp);
                return new ResponseDto<DetailTableDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto)
        {
            try
            {
                var validate = await _updateTableValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes = ErrorCodes.ValidationError };
                }
                //bura için ayrı bi repository yazacam
                //var checkTable = await _tableRepository.GetByIdAsync(dto.TableNumber);
                var rp = await _tableRepository.GetByIdAsync(dto.Id);
                if (rp == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Masa Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
                }
                var result = _mapper.Map(dto, rp);
                await _tableRepository.UpdateAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarıyla Güncellendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
        }
    }
}
