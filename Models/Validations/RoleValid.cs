using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebBase.Data.Entities;
using WebBase.Models.RequestModels;

namespace WebBase.Models.Validations
{
    public class RoleCreateValid : AbstractValidator<RoleCreateModel>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleCreateValid(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên nhóm quyền không được bỏ trống!")
                   .MinimumLength(5).WithMessage("Tên nhóm quyền có ít nhất 5 ký tự")
                   .Must(IsNameUnique).WithMessage("Tên nhóm quyền đã tồn tại!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả nhóm quyền không được bỏ trống!")
                .MaximumLength(250).WithMessage("Mô tả nhóm quyền có tối đa 250 ký tự!")
                .MinimumLength(5).WithMessage("Mô tả nhóm quyền có ít nhất 5 ký tự");
        }

        /// <summary>
        /// true thì pass qua must
        /// flase thì trả về msg
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public bool IsNameUnique(string newValue)
        {
            var result = _roleManager.FindByNameAsync(newValue).Result;
            if (result == null)
                return true;
            return false;
        }
    }

    public class RoleUpdateValid : AbstractValidator<RoleUpdateModel>
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleUpdateValid(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên nhóm quyền không được bỏ trống!")
                   .MinimumLength(5).WithMessage("Tên nhóm quyền có ít nhất 5 ký tự")
                   .Must(IsNameUpdateUnique).WithMessage("Tên nhóm quyền đã tồn tại!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả nhóm quyền không được bỏ trống!")
                .MaximumLength(250).WithMessage("Mô tả nhóm quyền có tối đa 250 ký tự!")
                .MinimumLength(5).WithMessage("Mô tả nhóm quyền có ít nhất 5 ký tự");
        }

        public bool IsNameUpdateUnique(RoleUpdateModel entity, string newValue)
        {
            var result = _roleManager.FindByIdAsync(entity.Id).Result;
            if (result.Name == newValue) // không thay đổi tên
                return true;
            else
            {
                // đã thay đổi tên mà tên thì kiểm tra ồn tại tên mới trong db k
                var findName = _roleManager.FindByNameAsync(newValue).Result;
                if (result == null)
                    return true;
            }
            return false;
        }
    }
}