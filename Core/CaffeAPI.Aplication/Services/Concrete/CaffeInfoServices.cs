using AutoMapper;
using CaffeAPI.Aplication.Dtos.CaffeInfoDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Services.Abstract;
using CaffeAPI.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Concrete
{
    public class CaffeInfoServices : ICaffeInfoServices
    {
        private readonly IGenericRepository<CaffeInfo> _caffeInfoRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCaffeInfoDto> _createCaffeInfoValidator;
        private readonly IValidator<UpdateCaffeInfoDto> _updateCaffeInfoValidator;

        public CaffeInfoServices(IGenericRepository<CaffeInfo> caffeInfoRepository, IMapper mapper, IValidator<CreateCaffeInfoDto> createCaffeInfoValidator, IValidator<UpdateCaffeInfoDto> updateCaffeInfoValidator)
        {
            _caffeInfoRepository = caffeInfoRepository;
            _mapper = mapper;
            _createCaffeInfoValidator = createCaffeInfoValidator;
            _updateCaffeInfoValidator = updateCaffeInfoValidator;
        }

        public async Task<ResponseDto<object>> AddCaffeInfo(CreateCaffeInfoDto dto)
        {
            try
            {
                var validate = await _createCaffeInfoValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault(), ErrorCode = ErrorCodes.ValidationError };
                }
                var caffeInfo = _mapper.Map<CaffeInfo>(dto);
                await _caffeInfoRepository.AddAsync(caffeInfo);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kafe bilgisi başarıyla eklendi" };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> DeleteCaffeInfo(int id)
        {
            try
            {
                var caffeInfo = await _caffeInfoRepository.GetByIdAsync(id);
                if (caffeInfo == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Kafe bilgisi bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                await _caffeInfoRepository.DeleteAsync(caffeInfo);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kafe bilgisi başarıyla silindi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultCaffeInfoDto>>> GetAllCafeInfos()
        {
            try
            {
                var caffeInfos = await _caffeInfoRepository.GetAllAsync();
                if (caffeInfos == null || !caffeInfos.Any())
                {
                    return new ResponseDto<List<ResultCaffeInfoDto>> { Success = false, Data = null, Message = "Kafe bilgisi bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultCaffeInfoDto>>(caffeInfos);
                return new ResponseDto<List<ResultCaffeInfoDto>> { Success = true, Data = result };

            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCaffeInfoDto>> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailCaffeInfoDto>> GetByIdCafeInfo(int id)
        {
            try
            {
                var caffeInfo = await _caffeInfoRepository.GetByIdAsync(id);
                if (caffeInfo == null)
                {
                    return new ResponseDto<DetailCaffeInfoDto> { Success = false, Data = null, Message = "Kafe bilgisi bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailCaffeInfoDto>(caffeInfo);
                return new ResponseDto<DetailCaffeInfoDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailCaffeInfoDto> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateCaffeInfo(UpdateCaffeInfoDto dto)
        {
            try
            {
                var validate = await _updateCaffeInfoValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault(), ErrorCode = ErrorCodes.ValidationError };
                }
                var caffeInfo = await _caffeInfoRepository.GetByIdAsync(dto.Id);
                if (caffeInfo == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Kafe bilgisi bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map(dto, caffeInfo);
                await _caffeInfoRepository.UpdateAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kafe bilgisi başarıyla güncellendi" };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
