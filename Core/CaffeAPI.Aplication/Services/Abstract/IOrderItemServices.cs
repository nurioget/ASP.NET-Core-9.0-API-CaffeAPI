using CaffeAPI.Aplication.Dtos.OrderItemDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Abstract
{
    public interface IOrderItemServices
    {
        Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems();
        Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id);
        Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto);
        Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto);
        Task<ResponseDto<object>> DeleteOrderItem(int id);
    }
}
