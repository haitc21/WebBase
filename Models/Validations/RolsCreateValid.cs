using FluentValidation;
using WebBase.Models.RequestModels;

namespace WebBase.Models.Validations
{
    public class RolsCreateValid : AbstractValidator<RolsCreateModel>
    {
        public RolsCreateValid()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Role name is required!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Role Description is required!")
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters!");
        }
    }
}