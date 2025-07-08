using CaffeAPI.Aplication.Dtos.OrderDtos;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [Authorize(Roles = "admin,employe")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderServices.GetAllOrder();
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderServices.GetOrderById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto)
        {
            var result = await _orderServices.AddOrder(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var result = await _orderServices.UpdateOrder(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);
            return CreateResponse(result);

        }


        [Authorize(Roles = "admin,employe")]
        [HttpGet("withdetails")]
        public async Task<IActionResult> GetAllOrderWithDetail()
        {
            var result = await _orderServices.GetAllOrderWithDetail();
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/hazir")]
        public async Task<IActionResult> UpdateOrderByStatusHazir(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/teslimedildi")]
        public async Task<IActionResult> UpdateOrderByStatusTeslimEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/iptaledildi")]
        public async Task<IActionResult> UpdateOrderByStatusİptalEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusİptalEdildi(orderId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/odendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusOdendi(orderId);
            return CreateResponse(result);
        }



    }

}

