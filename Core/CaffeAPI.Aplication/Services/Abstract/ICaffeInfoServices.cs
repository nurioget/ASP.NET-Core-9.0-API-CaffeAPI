using CaffeAPI.Aplication.Dtos.CaffeInfoDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Abstract
{
    public interface ICaffeInfoServices
    {
        Task<ResponseDto<List<ResultCaffeInfoDto>>> GetAllCafeInfos();
        Task<ResponseDto<DetailCaffeInfoDto>> GetByIdCafeInfo(int id);
        Task<ResponseDto<object>> AddCaffeInfo(CreateCaffeInfoDto dto);
        Task<ResponseDto<object>> UpdateCaffeInfo(UpdateCaffeInfoDto dto);
        Task<ResponseDto<object>> DeleteCaffeInfo(int id);
    }
}
