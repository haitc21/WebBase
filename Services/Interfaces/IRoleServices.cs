using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBase.Data.Entities;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;

namespace WebBase.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<IdentityResult> CreateRole(RolsCreateModel roleCM, string id);

        Task<List<RoleVM>> GetAllRoles();

        Task<Pagination<RoleVM>> GetRolePagging(string filter, int pageIndex = 1, int pageSize = 10);

        Task<RoleVM> GetRoleById(string id);

        Task<IdentityResult> UpdateRole(AppRole role, RoleVM roleVM);
    }
}