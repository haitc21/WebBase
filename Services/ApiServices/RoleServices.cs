using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Data.Entities;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Interfaces;

namespace WebBase.Services.ApiServices
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleServices(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRole(RolsCreateModel roleCM, string id)
        {
            var role = new AppRole()
            {
                Id = id,
                Name = roleCM.Name,
                Description = roleCM.Description,
                NormalizedName = roleCM.Name.ToUpper()
            };
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> DeleteRole(AppRole role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }

        public async Task<AppRole> FindById(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            if (result == null)
                return null;
            return result;
        }

        public async Task<List<RoleVM>> GetAllRoles()
        {
            var roleVM = await _roleManager.Roles
              .Select(r => new RoleVM()
              {
                  Id = r.Id,
                  Name = r.Name,
                  Description = r.Description
              }).ToListAsync();
            return roleVM;
        }

        public async Task<RoleVM> GetRoleById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return null;
            var roleVM = new RoleVM()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
            return roleVM;
        }

        public async Task<Pagination<RoleVM>> GetRolePagging(string filter, int pageIndex = 1, int pageSize = 10)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                query = query.Where(r => r.Name.ToLower().Contains(filter)
                || r.Description.ToLower().Contains(filter));
            }
            int totalRecords = query.Count();
            var itemss = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                 .Select(r => new RoleVM()
                 {
                     Id = r.Id,
                     Name = r.Name,
                     Description = r.Description
                 }).ToListAsync();
            var pagination = new Pagination<RoleVM>()
            {
                items = itemss,
                totalRecords = totalRecords
            };
            return pagination;
        }

        /// <summary>
        /// -2 not found, 0 update fall, 1 Succeeded
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleVM"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateRole(AppRole role, RoleVM roleVM)
        {
            role.Name = roleVM.Name;
            role.Description = roleVM.Description;
            role.NormalizedName = roleVM.Name.ToUpper();
            var rel = await _roleManager.UpdateAsync(role);
            return rel;
        }
    }
}