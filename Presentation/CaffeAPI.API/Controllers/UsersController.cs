using CaffeAPI.Aplication.Dtos.UserDtos;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _userServices.Register(dto);
            return CreateResponse(result);
        }

        [HttpPost("CreateRole")]    
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _userServices.CreateRole(roleName);
            return CreateResponse(result);
        }

        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string email, string roleName)
        {
            var result = await _userServices.AddToRole(email, roleName);
            return CreateResponse(result);
        }
    }
}
