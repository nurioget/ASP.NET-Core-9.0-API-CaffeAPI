using CaffeAPI.Aplication.Dtos.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Validators.Category
{
    public class AddCategoryValidator:AbstractValidator<CreateCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Kategorinin adı boş olamaz.")
                .Length(3,30).WithMessage("Kategori adinin uzunluğu 3 ila 30 arasında olması gerekir.");
        }
    }
}
