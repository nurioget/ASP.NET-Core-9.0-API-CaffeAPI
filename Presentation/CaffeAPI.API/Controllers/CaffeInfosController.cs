using CaffeAPI.Aplication.Dtos.CaffeInfoDtos;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/cafeinfos")]
    [ApiController]
    public class CaffeInfosController : BaseController
    {
        private readonly ICaffeInfoServices _caffeInfoService;

        public CaffeInfosController(ICaffeInfoServices caffeInfoService)
        {
            _caffeInfoService = caffeInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCafeInfos()
        {
            var result = await _caffeInfoService.GetAllCafeInfos();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCafeInfo(int id)
        {
            var result = await _caffeInfoService.GetByIdCafeInfo(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCaffeInfo(CreateCaffeInfoDto dto)
        {
            var result = await _caffeInfoService.AddCaffeInfo(dto);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCaffeInfo(UpdateCaffeInfoDto dto)
        {
            var result = await _caffeInfoService.UpdateCaffeInfo(dto);
            return CreateResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCaffeInfo(int id)
        {
            var result = await _caffeInfoService.DeleteCaffeInfo(id);
            return CreateResponse(result);
        }
    }
}
