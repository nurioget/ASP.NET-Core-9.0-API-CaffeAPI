using CaffeAPI.Aplication.Dtos.CategoryDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories();
        Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id);
        Task <ResponseDto<object>>AddCategory(CreateCategoryDto dto);
        Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto);
        Task<ResponseDto<object>> DeleteCategory(int id);
        Task<ResponseDto<List<ResultCategoriesWithMenuDto>>> GetCategoriesWithMenuItems();
    }
}

