using FluentValidation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebBase.Models.RequestModels;

namespace WebBase.Models.Validations
{
    public class RolsCreateValid : AbstractValidator<RoleCreateModel>
    {
        private readonly IEnumerable<RoleCreateModel> _role;
        public RolsCreateValid(IEnumerable<RoleCreateModel> role)
        {
            _role = role;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên nhóm quyền không được bỏ trống!")
                   .MinimumLength(5).WithMessage("Tên nhóm quyền có ít nhất 5 ký tự")
                   .Must(IsNameUnique).WithMessage("Tên nhóm quyền đã tồn tại!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả nhóm quyền không được bỏ trống!")
                .MaximumLength(250).WithMessage("Mô tả nhóm quyền có tối đa 250 ký tự!")
                .MinimumLength(5).WithMessage("Mô tả nhóm quyền có ít nhất 5 ký tự");
        }

        public bool IsNameUnique(string newValue)
        {
            var result =  _role.Any(r => r.Name == newValue);
            return result;
        }
    }
}