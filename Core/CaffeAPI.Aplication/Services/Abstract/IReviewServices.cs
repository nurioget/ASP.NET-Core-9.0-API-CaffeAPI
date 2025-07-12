using CaffeAPI.Aplication.Dtos.ResponseDtos;
using CaffeAPI.Aplication.Dtos.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Services.Abstract
{
    public interface IReviewServices
    {
        Task<ResponseDto<List<ResultReviewDto>>> GetAllReviews();
        Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id);
        Task<ResponseDto<object>> AddReview(CreateReviewDto dto);
        Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto);
        Task<ResponseDto<object>> DeleteReview(int id);
    }
}
