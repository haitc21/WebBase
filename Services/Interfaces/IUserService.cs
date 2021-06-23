using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBase.Data.Entities;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;

namespace WebBase.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUser(UserCreateModel userCM, string id);

        Task<List<UserVM>> GetAllUsers();

        Task<Pagination<UserVM>> GetUserPagging(string filter, int pageIndex = 1, int pageSize = 10);

        Task<UserVM> GetUserById(string id);

        Task<AppUser> FindById(string id);

        Task<IdentityResult> UpdateUser(AppUser user, UserUpdateModel userUM);

        Task<IdentityResult> DeleteUser(AppUser user);

        Task<List<FunctionVM>> GetMenu(string userId);

        Task<IdentityResult> ChangePassword(AppUser user, UserChangePasswordModel userCPM);
    }
}