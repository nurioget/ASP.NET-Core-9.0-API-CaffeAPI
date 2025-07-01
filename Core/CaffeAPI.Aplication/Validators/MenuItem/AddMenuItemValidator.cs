using CaffeAPI.Aplication.Dtos.MenuItemDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.MenuItem
{
    public class AddMenuItemValidator : AbstractValidator<CreateMenuItemDto>
    {
        public AddMenuItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Menu Item adı boş olamaz.")
                .Length(2, 50).WithMessage("Menu Item adı 2 ile 50 karakter arasında olmalıdır.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Menu Item açıklaması boş olamaz.")
                .Length(5, 100).WithMessage("Menu Item açıklaması 5 ile 200 karakter arasında olmalıdır.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Menu Item fiyatı sıfırdan büyük olmalıdır.");
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Menu Item görseli boş olamaz.");
        }
    }
}
