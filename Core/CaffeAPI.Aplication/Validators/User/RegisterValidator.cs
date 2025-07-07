using CaffeAPI.Aplication.Dtos.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.User
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ad alanı boş olamaz.")
                .Length(2, 50)
                .WithMessage("Ad alanı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Soyad alanı boş olamaz.")
                .Length(2, 50)
                .WithMessage("Soyad alanı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email alanı boş olamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir email adresi giriniz.");

            //RuleFor(x => x.Password)
            //    .NotEmpty()
            //    .WithMessage("Şifre alanı boş olamaz.")
            //    .MinimumLength(6)
            //    .WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.")
            //    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")
            //    .WithMessage("Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
        }
    }
}
