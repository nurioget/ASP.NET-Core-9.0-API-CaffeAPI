using AutoMapper;
using CaffeAPI.Aplication.Dtos.OrderItemDtos;
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
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateOrderItemValidator;

        public OrderItemServices(IGenericRepository<OrderItem> orderItemRepository, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemValidator, IValidator<UpdateOrderItemDto> updateOrderItemValidator)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _createOrderItemValidator = createOrderItemValidator;
            _updateOrderItemValidator = updateOrderItemValidator;
        }

        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
        {
            try
            {
                var validate=await _createOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",",validate.Errors.Select(x=>x.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
                }
                var result= _mapper.Map<OrderItem>(dto);    
                await _orderItemRepository.AddAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş öğesi başarıyla eklendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            try
            {
                var checkOrderItem = await _orderItemRepository.GetByIdAsync(id);
                if (checkOrderItem == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş öğesi bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                await _orderItemRepository.DeleteAsync(checkOrderItem);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş öğesi başarıyla silindi"  };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var db = await _orderItemRepository.GetAllAsync();
                if (db.Count==0)
                {
                    return new ResponseDto<List<ResultOrderItemDto>> { Success = false, Data = null, Message = "Sipariş bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultOrderItemDto>>(db);
                return new ResponseDto<List<ResultOrderItemDto>> { Success = true, Data = result};
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderItemDto>> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id)
        {
            try
            {
                var db = await _orderItemRepository.GetByIdAsync(id);
                if (db == null)
                {
                    return new ResponseDto<DetailOrderItemDto> { Success = false, Data = null, Message = "Sipariş bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailOrderItemDto>(db);
                return new ResponseDto<DetailOrderItemDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderItemDto> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }

        }

        public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            try
            {
                var validate = await _updateOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
                }
                var checkOrderItem= await _orderItemRepository.GetByIdAsync(dto.Id);
                if (checkOrderItem == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş öğesi bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map(dto,checkOrderItem);
                await _orderItemRepository.UpdateAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş öğesi başarıyla güncellendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
