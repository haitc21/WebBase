using FluentValidation;
using WebBase.Models.RequestModels;

namespace WebBase.Models.Validations
{
    public class UserCreateValid : AbstractValidator<UserCreateModel>
    {
        public UserCreateValid()
        {
            // password configed in statup.cs

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required!")
                .EmailAddress().WithMessage("Email invalid!");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required!");
            RuleFor(x => x.Dob).NotEmpty().WithMessage("Date of birdth is required!");
        }
    }
}