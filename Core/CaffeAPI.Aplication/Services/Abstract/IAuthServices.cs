using CaffeAPI.Aplication.Dtos.AuthDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Abstract
{
    public interface IAuthServices
    {
        Task<ResponseDto<object>> GenereteToken(TokenDto dto);
    }
}
