using Course.Web.Models.DiscountModels;
using FluentValidation;

namespace Course.Web.Validators.DiscountValidators
{
    public class DiscountApplyVMValidator:AbstractValidator<DiscountApplyVM>
    {
        public DiscountApplyVMValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("indirim kupon alanı boş olamaz");
        }
    }
}
