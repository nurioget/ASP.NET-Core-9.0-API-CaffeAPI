using CaffeAPI.Aplication.Dtos.AuthDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Dtos.UserDtos;
using CaffeAPI.Aplication.Helpers;
using CaffeAPI.Aplication.Interfaces;
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
        private readonly IUserRepository _userRepository;

        public AuthServices(TokenHelpers tokenHelpers, IUserRepository userRepository)
        {
            _tokenHelpers = tokenHelpers;
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<object>> GenereteToken(LoginDto dto)
        {
            try
            {
                //var checkUser = dto.Email == "admin@admin.com" ? true : false;
                var checkUser = await _userRepository.CheckUser(dto.Email);
                if (checkUser.Id != null)
                {
                    var user = await _userRepository.CheckUserWithPassword(dto);
                    if (user.Succeeded)
                    {
                        var tokenDto = new TokenDto
                        {
                            Email = dto.Email,
                            Id = checkUser.Id,
                            Role = checkUser.Role,
                        };
                        string token = _tokenHelpers.GenereteToken(tokenDto);
                        return new ResponseDto<object> { Success = true, Data = new { token = token } };
                    }
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Kullanıcı Bulunamadı", ErrorCode = ErrorCodes.Unauthorized };
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
