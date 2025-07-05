using CaffeAPI.Aplication.Dtos.AuthDtos;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("genereteToken")]
        public async Task<IActionResult> GenereteToken(TokenDto dto)
        {
            var result = await _authServices.GenereteToken(dto);
            return CreateResponse(result);
        }
    }
}