using FluentValidation;
using WebBase.Models.RequestModels;

namespace WebBase.Models.Validations
{
    public class UserUpdateValid : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateValid()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalid!");
            RuleFor(x => x.FirstName).MaximumLength(250).WithMessage("First name maximum length is 250 charater!");
            RuleFor(x => x.LastName).MaximumLength(250).WithMessage("Last name maximum length is 250 charater!");
        }
    }
}