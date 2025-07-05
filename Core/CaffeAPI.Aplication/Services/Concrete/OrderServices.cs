using AutoMapper;
using CaffeAPI.Aplication.Dtos.OrderDtos;
using CaffeAPI.Aplication.Dtos.OrderItemDtos;
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
    public class OrderServices : IOrderServices
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IOrderRepository _orderRepository2;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IValidator<UpdateOrderDto> _updateOrderValidator;

        public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator, IGenericRepository<OrderItem> orderItemRepository, IOrderRepository orderRepository2, IGenericRepository<MenuItem> menuItemRepository, IGenericRepository<Table> tableRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _createOrderValidator = createOrderValidator;
            _updateOrderValidator = updateOrderValidator;
            _orderItemRepository = orderItemRepository;
            _orderRepository2 = orderRepository2;
            _menuItemRepository = menuItemRepository;
            _tableRepository = tableRepository;
        }

        public async Task<ResponseDto<object>> AddOrder(CreateOrderDto dto)
        {
            try
            {
                var validate = await _createOrderValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
                }
                var result = _mapper.Map<Order>(dto);
                result.Status = OrderStatus.Hazirlaniyor;
                result.CreatedAt = DateTime.Now;
                decimal totalPrice = 0;
                foreach (var item in result.OrderItems)
                {
                    item.MenuItem=await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price=item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                result.TotalPrice = totalPrice;
                await _orderRepository.AddAsync(result);
                var table = await _tableRepository.GetByIdAsync(dto.TableId);
                table.IsActive = false;
                await _tableRepository.UpdateAsync(table);

                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Başarıyla Eklendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        //public async Task<ResponseDto<object>> AddOrderItemByOrder(AddOrderItemByOrderDto dto)
        //{
        //    try
        //    {
 
        //        var orderdb = await _orderRepository.GetByIdAsync(dto.OrderId);
        //        var orderItems=await _orderItemRepository.GetAllAsync();
        //        if (orderdb == null)
        //        {
        //            return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
        //        }
        //        var result = _mapper.Map<OrderItem>(dto.OrderItem);
        //        orderdb.OrderItems.Add(result);
        //        await _orderRepository.UpdateAsync(orderdb);
        //        return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Başarıyla Güncellendi" };
        //    }
        //    catch (Exception)
        //    {
        //        return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
        //    }
        //}

        public async Task<ResponseDto<object>> DeleteOrder(int orderId)
        {
            try
            {
                var orderdb = await _orderRepository.GetByIdAsync(orderId);
                if (orderdb == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                await _orderRepository.DeleteAsync(orderdb);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Başarıyla Silindi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder()
        {
            try
            {
                var orderdb = await _orderRepository.GetAllAsync();
                var orderItemDb = await _orderItemRepository.GetAllAsync();
                if (orderdb.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orderdb);
                return new ResponseDto<List<ResultOrderDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderWithDetail()
        {
            try
            {
                var orderdb = await _orderRepository2.GetAllOrderWithDetailAsync();
                if (orderdb.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orderdb);
                return new ResponseDto<List<ResultOrderDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailOrderDto>> GetOrderById(int orderId)
        {
            try
            {
                var orderdb = await _orderRepository2.GetOrderByIdWithDetailAsync(orderId);
                if (orderdb == null)
                {
                    return new ResponseDto<DetailOrderDto> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailOrderDto>(orderdb);
                return new ResponseDto<DetailOrderDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderDto> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
        {
            try
            {
                var validate = await _updateOrderValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
                }
                var orderdb = await _orderRepository.GetByIdAsync(dto.Id);
                if (orderdb == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }

                var result = _mapper.Map(dto, orderdb);
                result.UpdateAt = DateTime.Now;
                decimal totalPrice = 0;
                foreach (var item in result.OrderItems)
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                result.TotalPrice = totalPrice;

                await _orderRepository.UpdateAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Başarıyla Güncellendi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId)
        {
            try
            {
               
                var orderdb = await _orderRepository.GetByIdAsync(orderId);
                if (orderdb == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                orderdb.Status=OrderStatus.Hazir;
                await _orderRepository.UpdateAsync(orderdb);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Durumu Hazır Olarak Güncellendi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            try
            {

                var orderdb = await _orderRepository.GetByIdAsync(orderId);
                if (orderdb == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                orderdb.Status = OrderStatus.TeslimEdildi;
                await _orderRepository.UpdateAsync(orderdb);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Durumu Teslim Edildi Olarak Güncellendi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusİptalEdildi(int orderId)
        {
            try
            {

                var orderdb = await _orderRepository.GetByIdAsync(orderId);
                if (orderdb == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                orderdb.Status = OrderStatus.IptalEdildi;
                await _orderRepository.UpdateAsync(orderdb);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Durumu İptal edildi Olarak Güncellendi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusOdendi(int orderId)
        {
            try
            {

                var orderdb = await _orderRepository.GetByIdAsync(orderId);
                if (orderdb == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                orderdb.Status = OrderStatus.Odendi;
                await _orderRepository.UpdateAsync(orderdb);
                var table = await _tableRepository.GetByIdAsync(orderdb.TableId);   
                table.IsActive = true;
                await _tableRepository.UpdateAsync(table);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Sipariş Durumu Ödendi Olarak Güncellendi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Sorun Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

    }
}
