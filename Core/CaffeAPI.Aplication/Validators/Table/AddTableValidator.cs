using CaffeAPI.Aplication.Dtos.TablesDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.Table
{
    public class AddTableValidator : AbstractValidator<CreateTableDto>
    {
        public AddTableValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty()
                .WithMessage("Masa Numarası Boş Bırakılamaz")
                .GreaterThan(0)
                .WithMessage("Masa Numarası 0'dan Büyük Olmalıdır");
            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Kapasite Boş Bırakılamaz")
                .GreaterThan(0)
                .WithMessage("Kapasite 0'dan Büyük Olmalıdır");

        }
    }
}
