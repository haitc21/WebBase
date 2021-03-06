using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Data;
using WebBase.Data.Entities;
using WebBase.Helpers;
using WebBase.Helpers.Authorization;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Interfaces;
using static WebBase.Common.Enums;

namespace WebBase.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IRoleServices _roleServices;
        private readonly ApplicationDbContext _context;

        public RolesController(IRoleServices roleServices,
            ApplicationDbContext context)
        {
            _roleServices = roleServices;
            _context = context;
        }

        // URL: POST https://localhost:5000/api/roles
        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<ActionResult> PostRole(RoleCreateModel roleCM)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse("Modelstate invalid!"));
            var id = Guid.NewGuid().ToString();
            var result = await _roleServices.CreateRole(roleCM, id);
            if (result.Succeeded)
                return CreatedAtAction(nameof(GetById), new { id = id }, roleCM);
            else
                return BadRequest(new ApiBadRequestResponse(result));
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<ActionResult> GetRoles()
        {
            var roleVM = await _roleServices.GetAllRoles();
            if (roleVM == null)
                return NotFound(new ApiNotFoundResponse("Can't found any role"));
            return Ok(roleVM);
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<ActionResult> GetRole(string filter, int pageIndex = 1, int pageSize = 10)
        {
            var pagination = await _roleServices.GetRolePagging(filter, pageIndex, pageSize);
            if (pagination.totalRecords == 0)
                return NotFound(new ApiNotFoundResponse($"Can't found role wirh filter: {filter}"));
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<ActionResult> GetById(string id)
        {
            var roleVM = await _roleServices.GetRoleById(id);
            if (roleVM == null)
                return NotFound(new ApiNotFoundResponse($"Role id {id} not exsited!"));
            return Ok(roleVM);
        }

        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<ActionResult> PutRole(string id, [FromBody] RoleUpdateModel roleUM)
        {
            if (id != roleUM.Id.ToString())
                return BadRequest(new ApiBadRequestResponse("Role id from url diffirent role id from body!"));
            var role = await _roleServices.FindById(id);
            if (role == null)
                return NotFound(new ApiNotFoundResponse($"Role id {id} not exsited!"));
            var rel = await _roleServices.UpdateRole(role, roleUM);
            if (rel.Succeeded)
            {
                return Ok(roleUM);
            }
            return BadRequest(new ApiBadRequestResponse(rel));
        }

        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.DELETE)]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await _roleServices.FindById(id);
            if (role == null)
                return NotFound(new ApiNotFoundResponse($"Role id {id} not exsited!"));
            var roleVM = new RoleVM()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
            var rel = await _roleServices.DeleteRole(role);
            if (rel.Succeeded)
            {
                return Ok(roleVM);
            }
            return BadRequest(new ApiBadRequestResponse(rel));
        }

        [HttpGet("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.VIEW)]
        public async Task<IActionResult> GetPermissionByRoleId(string roleId)
        {
            var permissions = from p in _context.Permissions
                              join a in _context.Commands
                              on p.CommandId equals a.Id
                              where p.RoleId == roleId
                              select new PermissionVM()
                              {
                                  FunctionId = p.FunctionId,
                                  CommandId = p.CommandId,
                                  RoleId = p.RoleId
                              };

            return Ok(await permissions.ToListAsync());
        }

        [HttpPut("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PutPermissionByRoleId(string roleId, [FromBody] PẻmissionUpdateModel request)
        {
            //create new permission list from user changed
            var newPermissions = new List<Permission>();
            foreach (var p in request.Permissions)
            {
                newPermissions.Add(new Permission(p.FunctionId, roleId, p.CommandId));
            }

            var existingPermissions = _context.Permissions.Where(x => x.RoleId == roleId);
            _context.Permissions.RemoveRange(existingPermissions);
            _context.Permissions.AddRange(newPermissions.Distinct(new MyPermissionComparer()));
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse("Update permission failed!"));
        }
    }
    /// <summary>
    /// So sanh 2 đối tượng permission
    /// giống như trên code dạo
    /// khái niệm deep comparer
    /// </summary>
    internal class MyPermissionComparer : IEqualityComparer<Permission>
    {
        // Items are equal if their ids are equal.
        public bool Equals(Permission x, Permission y)
        {
            // Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the items properties are equal.
            return x.CommandId == y.CommandId && x.FunctionId == x.FunctionId && x.RoleId == x.RoleId;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Permission permission)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(permission, null)) return 0;

            //Get hash code for the ID field.
            int hashProductId = (permission.CommandId + permission.FunctionId + permission.RoleId).GetHashCode();

            return hashProductId;
        }
    }
}