using CaffeAPI.Aplication.Dtos.OrderDtos;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderServices.GetAllOrder();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderServices.GetOrderById(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto)
        {
            var result = await _orderServices.AddOrder(dto);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var result = await _orderServices.UpdateOrder(dto);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);
            return CreateResponse(result);

        }

        [HttpGet("GetAllOrderWithDetail")]
        public async Task<IActionResult> GetAllOrderWithDetail()
        {
            var result = await _orderServices.GetAllOrderWithDetail();
            return CreateResponse(result);
        }

        [HttpPut("UpdateOrderStatusHazir")]
        public async Task<IActionResult> UpdateOrderByStatusHazir(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(result);
        }

        [HttpPut("UpdateOrderStatusTeslimEdildi")]
        public async Task<IActionResult> UpdateOrderByStatusTeslimEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(result);
        }

        [HttpPut("UpdateOrderStatusİptalEdildi")]
        public async Task<IActionResult> UpdateOrderByStatusİptalEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusİptalEdildi(orderId);
            return CreateResponse(result);
        }

        //[HttpPut("AddOrderItemByOrder")]
        //public async Task<IActionResult> AddOrderItemByOrder(AddOrderItemByOrderDto dto)
        //{
        //    var result = await _orderServices.AddOrderItemByOrder(dto);
        //    return CreateResponse(result);
        //}

        [HttpPut("UpdateOrderStatusOdendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusOdendi(orderId);
            return CreateResponse(result);
        }



    }

}

