using CaffeAPI.Aplication.Dtos.ReviewDtos;
using CaffeAPI.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaffeAPI.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        private readonly IReviewServices _reviewServices;

        public ReviewsController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _reviewServices.GetAllReviews();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var result = await _reviewServices.GetByIdReview(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto dto)
        {
            var result = await _reviewServices.AddReview(dto);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto dto)
        {
            var result = await _reviewServices.UpdateReview(dto);
            return CreateResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewServices.DeleteReview(id);
            return CreateResponse(result);

        }
    }
}
