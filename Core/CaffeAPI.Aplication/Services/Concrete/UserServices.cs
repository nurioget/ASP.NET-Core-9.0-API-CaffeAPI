using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Dtos.UserDtos;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Services.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Concrete
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterDto> _registerValidator;

        public UserServices(IUserRepository userRepository, IValidator<RegisterDto> registerValidator)
        {
            _userRepository = userRepository;
            _registerValidator = registerValidator;
        }

        public async Task<ResponseDto<object>> AddToRole(string email, string roleName)
        {
            try
            {
                var result = await _userRepository.AddRoleToUserAsync(email, roleName);
                if (result)
                    return new ResponseDto<object> { Success = true, Data = null, Message = "Rol Ataması Yapıldı" };

                return new ResponseDto<object> { Success = false, Data = null, Message = "Rol Ataması Yapılırken Hata Oluştu", ErrorCode = ErrorCodes.BadRequest };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> CreateRole(string roleName)
        {
            try
            {
                var result = await _userRepository.CreateRoleAsync(roleName);
                if (result)
                    return new ResponseDto<object> { Success = true, Data = null, Message = "Rol Oluşturuldu" };

                return new ResponseDto<object> { Success = false, Data = null, Message = "Rol Oluşturulurulurken Hata Oluştu", ErrorCode = ErrorCodes.BadRequest };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> Register(RegisterDto dto)
        {
            try
            {
                var validate = await _registerValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = validate.Errors.FirstOrDefault().ErrorMessage, ErrorCode = ErrorCodes.ValidationError, };
                }
                var result = await _userRepository.RegisterAsync(dto);

                if (result.Succeeded)
                {
                    return new ResponseDto<object> { Success = true, Data = null, Message = "Kayıt İşlemi Başarılı" };
                }
                else
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = result.Errors.FirstOrDefault().Description };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> RegisterDefault(RegisterDto dto)
        {
            try
            {
                var validate = await _registerValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = validate.Errors.FirstOrDefault().ErrorMessage, ErrorCode = ErrorCodes.ValidationError, };
                }
                var result = await _userRepository.RegisterAsync(dto);

                if (result.Succeeded)
                {
                    var roleResult = await _userRepository.AddRoleToUserAsync(dto.Email, "user");
                    if (roleResult)
                        return new ResponseDto<object> { Success = true, Data = null, Message = "Kayıt İşlemi Başarılı Bir Şekilde Gerçekleştirilmiştir" };
                    else
                        return new ResponseDto<object> { Success = false, Data = null, Message = "Kayıt İşlemi Başarılı, Ancak Rol Ataması Yapılamadı Lütfen Yetkiliyle İletişime Geçiniz", ErrorCode = ErrorCodes.BadRequest };
                }
                else
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = result.Errors.FirstOrDefault().Description };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
