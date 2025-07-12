using AutoMapper;
using CaffeAPI.Aplication.Dtos.CaffeInfoDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Interfaces;
using CaffeAPI.Aplication.Services.Abstract;
using CaffeAPI.Domain.Entities;
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

        public CaffeInfoServices(IGenericRepository<CaffeInfo> caffeInfoRepository, IMapper mapper)
        {
            _caffeInfoRepository = caffeInfoRepository;
            _mapper = mapper;
        }

        public Task<ResponseDto<object>> AddCaffeInfo(CreateCaffeInfoDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<object>> DeleteCaffeInfo(int id)
        {
            throw new NotImplementedException();
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

        public Task<ResponseDto<DetailCaffeInfoDto>> GetByIdCafeInfo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<object>> UpdateCaffeInfo(UpdateCaffeInfoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
