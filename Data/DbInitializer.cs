using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Common;
using WebBase.Data.Entities;

namespace WebBase.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly string AdminRoleName = "Admin";
        private readonly string UserRoleName = "Member";

        public DbInitializer(ApplicationDbContext context,
          UserManager<AppUser> userManager,
          RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            #region Quyền

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AdminRoleName,
                    NormalizedName = AdminRoleName.ToUpper(),
                    Description = "Quyền quản trị viên"
                });
                await _roleManager.CreateAsync(new AppRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = UserRoleName,
                    NormalizedName = UserRoleName.ToUpper(),
                    Description = "Quyền người dùng"
                });
            }

            #endregion Quyền

            #region Người dùng

            if (!_userManager.Users.Any())
            {
                var result = await _userManager.CreateAsync(new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    FirstName = "Quản trị",
                    LastName = "tranhai",
                    Email = "hai.tc21@gmail.com",
                    LockoutEnabled = false,
                    Status = (int)Enums.Status.Activied,
                    Dob = DateTime.Parse("1995/12/21")
                }, "Admin@123");
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("admin");
                    await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
            }

            #endregion Người dùng

            #region Chức năng

            if (!_context.Functions.Any())
            {
                _context.Functions.AddRange(new List<Function>
                {
                    new Function {Id = "DASHBOARD", Name = "DashBoard", ParentId = null, SortOrder = 1,Url = "/dashboard",Icon="fa-dashboard" },

                    new Function {Id = "STATISTIC",Name = "Thống kê", ParentId = null, Url = "/statistics",Icon="fa-bar-chart-o" },

                    new Function {Id = "STATISTIC_MONTHLY_NEWMEMBER",Name = "Đăng ký từng tháng",ParentId = "STATISTIC",SortOrder = 1,Url = "/statistics/monthly-registers",Icon = "fa-wrench"},

                    new Function {Id = "SYSTEM", Name = "Hệ thống", ParentId = null, Url = "/systems",Icon="fa-th-list" },

                    new Function {Id = "SYSTEM_USER", Name = "Người dùng",ParentId = "SYSTEM",Url = "/systems/users",Icon="fa-desktop"},
                    new Function {Id = "SYSTEM_ROLE", Name = "Nhóm quyền",ParentId = "SYSTEM",Url = "/systems/roles",Icon="fa-desktop"},
                    new Function {Id = "SYSTEM_FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",Url = "/systems/functions",Icon="fa-desktop"},
                    new Function {Id = "SYSTEM_PERMISSION", Name = "Quyền hạn",ParentId = "SYSTEM",Url = "/systems/permissions",Icon="fa-desktop"},
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Commands.Any())
            {
                _context.Commands.AddRange(new List<Command>()
                {
                    new Command(){Id = "VIEW", Name = "Xem"},
                    new Command(){Id = "CREATE", Name = "Thêm"},
                    new Command(){Id = "UPDATE", Name = "Sửa"},
                    new Command(){Id = "DELETE", Name = "Xoá"},
                    new Command(){Id = "APPROVE", Name = "Duyệt"},
                });
            }

            #endregion Chức năng

            var functions = _context.Functions;

            if (!_context.CommandInFunctions.Any())
            {
                foreach (var function in functions)
                {
                    var createAction = new CommandInFunction()
                    {
                        CommandId = "CREATE",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(createAction);

                    var updateAction = new CommandInFunction()
                    {
                        CommandId = "UPDATE",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(updateAction);
                    var deleteAction = new CommandInFunction()
                    {
                        CommandId = "DELETE",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(deleteAction);

                    var viewAction = new CommandInFunction()
                    {
                        CommandId = "VIEW",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(viewAction);
                }
            }

            if (!_context.Permissions.Any())
            {
                var adminRole = await _roleManager.FindByNameAsync(AdminRoleName);
                foreach (var function in functions)
                {
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "CREATE"));
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "UPDATE"));
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "DELETE"));
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "VIEW"));
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}