using CaffeAPI.Aplication.Dtos.CategoryDtos;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace CaffeAPI.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryServices _categoryServices;
        private readonly Serilog.ILogger _log;

        public CategoriesController(ICategoryServices categoryServices, Serilog.ILogger log)
        {
            _categoryServices = categoryServices;
            _log = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            _log.Information("Get-Categories method called ");
            var result = await _categoryServices.GetAllCategories();
            _log.Information("IGet-Categories method called " + result.Success);
            _log.Warning("wGet-Categories method called " + result.Success);
            _log.Error("EGet-Categories method called " + result.Success);
            _log.Debug("DGet-Categories method called " + result.Success);
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory([FromQuery]int id)
        {
            var result = await _categoryServices.GetByIdCategory(id);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto dto)
        {
            var result = await _categoryServices.AddCategory(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var result = await _categoryServices.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryServices.DeleteCategory(id);
            return CreateResponse(result);
        }

        [HttpGet("withmenuitems")]
        public async Task<IActionResult> GetAllCategoriesWithMenuItems()
        {
            var result = await _categoryServices.GetCategoriesWithMenuItems();
            return CreateResponse(result);
        }
    }
}