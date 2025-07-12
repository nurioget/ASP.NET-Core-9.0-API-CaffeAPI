using CaffeAPI.Aplication.Dtos.ReviewDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.Review
{
    public class AddReviewValidator : AbstractValidator<CreateReviewDto>
    {
        public AddReviewValidator()
        {
           
            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage("Yorum boş olamaz.")
                .Length(5,500)
                .WithMessage("Yorum 500 karakterden uzun olamaz.");

            RuleFor(x => x.Rating)
                .NotNull()
                .WithMessage("Yıldız değeri boş olamaz.")
                .InclusiveBetween(1, 5)
                .WithMessage("Yıldız değeri 1 ile 5 arasında olmalıdır.");

        }
    }
}
