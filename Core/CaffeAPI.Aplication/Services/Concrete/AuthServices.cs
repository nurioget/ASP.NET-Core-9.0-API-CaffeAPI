using CaffeAPI.Aplication.Dtos.AuthDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Helpers;
using CaffeAPI.Aplication.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Concrete
{
    public class AuthServices : IAuthServices
    {
        private readonly TokenHelpers _tokenHelpers;

        public AuthServices(TokenHelpers tokenHelpers)
        {
            _tokenHelpers = tokenHelpers;
        }

        public async Task<ResponseDto<object>> GenereteToken(TokenDto dto)
        {
            try
            {
                var checkUser = dto.Email == "admin@admin.com" ? true : false;
                if (checkUser)
                {
                    string token = _tokenHelpers.GenereteToken(dto);
                    return new ResponseDto<object> { Success = true, Data = token };
                }
                return new ResponseDto<object> { Success = false, Data = null, Message = "Kullanıcı Bulunamadı", ErrorCode = ErrorCodes.Unauthorized };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
