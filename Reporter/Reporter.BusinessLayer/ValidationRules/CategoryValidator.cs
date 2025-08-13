using FluentValidation;
using Reporter.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter.BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {

            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı boş geçilemez");
            RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("Lütfen en az 3 karakter veri girişi yapınız.");
            RuleFor(x => x.CategoryName).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter veri girişi yapınız.");
        }
    }
}