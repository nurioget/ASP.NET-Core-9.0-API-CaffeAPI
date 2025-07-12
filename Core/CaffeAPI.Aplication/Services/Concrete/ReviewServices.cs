using AutoMapper;
using CaffeAPI.Aplication.Dtos.CaffeInfoDtos;
using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Dtos.ReviewDtos;
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
    public class ReviewServices : IReviewServices
    {
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReviewDto> _createReviewValidator;
        private readonly IValidator<UpdateReviewDto> _updateReviewValidator;

        public ReviewServices(IGenericRepository<Review> reviewRepository, IMapper mapper, IValidator<CreateReviewDto> createReviewValidator, IValidator<UpdateReviewDto> updateReviewValidator)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _createReviewValidator = createReviewValidator;
            _updateReviewValidator = updateReviewValidator;
        }

        public async Task<ResponseDto<object>> AddReview(CreateReviewDto dto)
        {
            try
            {
                var validate = await _createReviewValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault(), ErrorCode = ErrorCodes.ValidationError };
                }
                var result = _mapper.Map<Review>(dto);
                await _reviewRepository.AddAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Yorum Başarıli Bir Şekilde Eklendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> DeleteReview(int id)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id);
                if (review == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Yorum Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                await _reviewRepository.DeleteAsync(review);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Yorum Başarıli Bir Şekilde Silindi"  };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultReviewDto>>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                var result = _mapper.Map<List<ResultReviewDto>>(reviews);
                return new ResponseDto<List<ResultReviewDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultReviewDto>> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id);
                if (review == null)
                {
                    return new ResponseDto<DetailReviewDto> { Success = false, Data = null, Message = "Yorum Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailReviewDto>(review);
                return new ResponseDto<DetailReviewDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailReviewDto> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto)
        {
            try
            {
                var validate = await _updateReviewValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault(), ErrorCode = ErrorCodes.ValidationError };
                }
                var review = await _reviewRepository.GetByIdAsync(dto.Id);
                if (review == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Yorum Bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map(dto, review);
                await _reviewRepository.UpdateAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Yorum Başarıli Bir Şekilde Güncellendi" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
