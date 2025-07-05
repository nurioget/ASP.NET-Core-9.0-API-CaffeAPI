using CaffeAPI.Aplication.Dtos.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.Order
{
    public class AddOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public AddOrderValidator()
        {
            RuleFor(x=>x.TotalPrice)
                .NotEmpty()
                .WithMessage("Toplam fiyat boş olamaz.")
                .GreaterThan(0)
                .WithMessage("Toplam fiyat 0'dan büyük olmalıdır.");


        }
    }
}
