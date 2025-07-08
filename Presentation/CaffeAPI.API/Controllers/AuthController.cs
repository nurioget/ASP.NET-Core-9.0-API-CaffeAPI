using CaffeAPI.Aplication.Dtos.AuthDtos;
using CaffeAPI.Aplication.Dtos.UserDtos;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost]
        public async Task<IActionResult> GenereteToken(LoginDto dto)
        {
            var result = await _authServices.GenereteToken(dto);
            return CreateResponse(result);
        }
    }
}