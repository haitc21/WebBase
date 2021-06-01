using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            var lstCmd = new List<Command>()
            {
               new Command() { Id = "VIEW", Name = "Xem" },
               new Command() { Id = "CREATE", Name = "Thêm" },
               new Command() { Id = "UPDATE", Name = "Sửa" },
               new Command() { Id = "DELETE", Name = "Xoá" },
               new Command() { Id = "APPROVE", Name = "Duyệt" }
            };
            builder.Entity<Command>().HasData(lstCmd);

            var adminRoleId = Guid.NewGuid();
            var auserRoleId = Guid.NewGuid();

            builder.Entity<AppRole>().HasData(
                new AppRole() { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN", Description = "Admin role" },
                new AppRole() { Id = auserRoleId, Name = "User", NormalizedName = "USER", Description = "User role" }
                );
            DateTime dob = DateTime.Now;
            builder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    FirstName = "Quản trị",
                    LastName = "tranhai",
                    Email = "hai.tc21@gmail.com",
                    PasswordHash = "aDMIN@123",
                    Dob = dob
                },
                 new AppUser()
                 {
                     Id = Guid.NewGuid(),
                     UserName = "TranHai",
                     FirstName = "Trần",
                     LastName = "Hải",
                     Email = "hai.tc21@gmail.com",
                     Dob = dob
                 }
               );
            var lstFuncts = new List<Function>()
            {
                    new Function { Id = "DASHBOARD", Name = "DashBoard", ParentId = null, SortOrder = 1, Url = "/dashboard", Icon = "fa-dashboard" },
                    new Function { Id = "SYSTEM", Name = "Hệ thống", ParentId = null, Url = "/systems", Icon = "fa-th-list" },
                    new Function { Id = "SYSTEM_USER", Name = "Người dùng", ParentId = "SYSTEM", Url = "/systems/users", Icon = "fa-desktop" },
                    new Function { Id = "SYSTEM_ROLE", Name = "Nhóm quyền", ParentId = "SYSTEM", Url = "/systems/roles", Icon = "fa-desktop" },
                    new Function { Id = "SYSTEM_FUNCTION", Name = "Chức năng", ParentId = "SYSTEM", Url = "/systems/functions", Icon = "fa-desktop" },
                    new Function { Id = "SYSTEM_PERMISSION", Name = "Quyền hạn", ParentId = "SYSTEM", Url = "/systems/permissions", Icon = "fa-desktop" }
            };
            builder.Entity<Function>().HasData(lstFuncts);

            var lstCmdInFuncts = new List<CommandInFunction>();
            foreach (var function in lstFuncts)
            {
                var createAction = new CommandInFunction()
                {
                    CommandId = "CREATE",
                    FunctionId = function.Id
                };
                lstCmdInFuncts.Add(createAction);

                var updateAction = new CommandInFunction()
                {
                    CommandId = "UPDATE",
                    FunctionId = function.Id
                };
                lstCmdInFuncts.Add(updateAction);
                var deleteAction = new CommandInFunction()
                {
                    CommandId = "DELETE",
                    FunctionId = function.Id
                };
                lstCmdInFuncts.Add(deleteAction);

                var viewAction = new CommandInFunction()
                {
                    CommandId = "VIEW",
                    FunctionId = function.Id
                };
                lstCmdInFuncts.Add(viewAction);
            }
            builder.Entity<CommandInFunction>().HasData(lstCmdInFuncts);

            var lstPermissions = new List<Permission>();
            foreach (var function in lstFuncts)
            {                                                             
                lstPermissions.Add(new Permission(function.Id, adminRoleId.ToString(), "CREATE"));
                lstPermissions.Add(new Permission(function.Id, adminRoleId.ToString(), "UPDATE"));
                lstPermissions.Add(new Permission(function.Id, adminRoleId.ToString(), "DELETE"));
                lstPermissions.Add(new Permission(function.Id, adminRoleId.ToString(), "VIEW"));
            }
            builder.Entity<Permission>().HasData(lstPermissions);

        }
    }
}
