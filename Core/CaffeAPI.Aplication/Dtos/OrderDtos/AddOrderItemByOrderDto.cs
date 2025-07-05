using CaffeAPI.Aplication.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Dtos.OrderDtos
{
    public class AddOrderItemByOrderDto
    {
        public int OrderId { get; set; }
        public CreateOrderItemDto OrderItem { get; set; }
    }
}
