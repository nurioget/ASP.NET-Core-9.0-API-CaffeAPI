using CaffeAPI.Aplication.Dtos.OrderItemDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.OrderItem
{
    public class AddOrderItemVaidator : AbstractValidator<CreateOrderItemDto>
    {
        public AddOrderItemVaidator()
        {
            RuleFor(x=>x.Quantity)
                .NotEmpty()
                .WithMessage("Sipariş miktarı boş olamaz.")
                .GreaterThan(0)
                .WithMessage("Sipariş miktarı 0'dan büyük olmalıdır.");

        }
    }
}
