using CaffeAPI.Aplication.Dtos.CaffeInfoDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.CaffeInfo
{
    public class AddCaffeInfoValidator : AbstractValidator<CreateCaffeInfoDto>
    {
        public AddCaffeInfoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
                .WithMessage("Laffe adi boş olamaz")
                .MaximumLength(100)
                .WithMessage("Kafe adı en fazla 100 karakter olmalıdır");
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Kafe adresi boş olamaz")
                .MaximumLength(500)
                .WithMessage("Kafe adresi en fazla 500 karakter olmalıdır");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Kafe telefon numarası boş olamaz")
                .Matches(@"^\+?[0-9]{10,15}$")
                .WithMessage("Kafe telefon numarası geçerli bir formatta olmalıdır (örn: +1234567890)");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Kafe e-posta adresi boş olamaz")
                .EmailAddress()
                .WithMessage("Kafe e-posta adresi geçerli bir formatta olmalıdır");
        }
    }
}
