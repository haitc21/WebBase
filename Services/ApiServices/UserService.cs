using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Data;
using WebBase.Data.Entities;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Interfaces;

namespace WebBase.Services.ApiServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUser(UserCreateModel userCM, string id)
        {
            var user = new AppUser()
            {
                Id = id,
                Email = userCM.Email,
                Dob = DateTime.Parse(userCM.Dob),
                UserName = userCM.UserName,
                PasswordHash = userCM.Password,
                LastName = userCM.LastName,
                FirstName = userCM.FirstName,
                PhoneNumber = userCM.PhoneNumber,
                CreateDate = DateTime.Now,
            };
            var result = await _userManager.CreateAsync(user, userCM.Password);
            return result;
        }

        public async Task<List<UserVM>> GetAllUsers()
        {
            var users2 = await _userManager.Users.ToListAsync();
            var users = await _userManager.Users.Select(u => MapUserToVM(u)).ToListAsync();
            return users;
        }

        public async Task<Pagination<UserVM>> GetUserPagging(string filter, int pageIndex = 1, int pageSize = 10)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                query = query.Where(r => r.UserName.ToLower().Contains(filter)
                || r.FirstName.ToLower().Contains(filter)
                || r.LastName.ToLower().Contains(filter)
                || r.Email.ToLower().Contains(filter)
                );
            }
            int totalRecord = query.Count();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                 .Select(u => MapUserToVM(u)).ToListAsync();
            var pagination = new Pagination<UserVM>()
            {
                item = items,
                totalRecord = totalRecord
            };
            return pagination;
        }

        public async Task<UserVM> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;
            var userVM = MapUserToVM(user);
            return userVM;
        }

        public async Task<IdentityResult> UpdateUser(AppUser user, UserUpdateModel userUM)
        {
            user.FirstName = userUM.FirstName;
            user.LastName = userUM.LastName;
            user.Email = userUM.Email;
            user.FirstName = userUM.FirstName;
            user.PhoneNumber = userUM.PhoneNumber;
            user.Dob = DateTime.Parse(userUM.Dob);
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<AppUser> FindById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;
            return user;
        }

        public async Task<IdentityResult> DeleteUser(AppUser user)
        {
            var rel = await _userManager.DeleteAsync(user);
            return rel;
        }

        public async Task<List<FunctionVM>> GetMenu(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var query = from f in _context.Functions
                        join p in _context.Permissions
                            on f.Id equals p.FunctionId
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        join a in _context.Commands
                            on p.CommandId equals a.Id
                        where roles.Contains(r.Name) && a.Id == "VIEW"
                        select new FunctionVM
                        {
                            Id = f.Id,
                            Name = f.Name,
                            Url = f.Url,
                            ParentId = f.ParentId,
                            SortOrder = f.SortOrder,
                        };
            var data = await query.Distinct()
                .OrderBy(x => x.ParentId)
                .ThenBy(x => x.SortOrder)
                .ToListAsync();
            return data;
        }

        public async Task<IdentityResult> ChangePassword(AppUser user, UserChangePasswordModel userCPM)
        {
            var result = await _userManager.ChangePasswordAsync(user, userCPM.CurPass, userCPM.NewPass);
            return result;
        }

        #region

        private static UserVM MapUserToVM(AppUser user)
        {
            var userVM = new UserVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Dob = user.Dob.ToString("dd/MM/yyyy"),
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return userVM;
        }

        #endregion
    }
}