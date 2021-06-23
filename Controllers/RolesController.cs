using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Data;
using WebBase.Data.Entities;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Authorization;
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
        public async Task<ActionResult> PostRole(RolsCreateModel roleCM)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var id = Guid.NewGuid().ToString();
            var result = await _roleServices.CreateRole(roleCM, id);
            if (result.Succeeded)
                return CreatedAtAction(nameof(GetById), new { id = id }, roleCM);
            else
                return BadRequest();
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<ActionResult> GetRoles()
        {
            var roleVM = await _roleServices.GetAllRoles();
            if (roleVM == null)
                return NotFound();
            return Ok(roleVM);
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<ActionResult> GetRole(string filter, int pageIndex = 1, int pageSize = 10)
        {
            var pagination = await _roleServices.GetRolePagging(filter, pageIndex, pageSize);
            if (pagination.totalRecord == 0)
                return NotFound();
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<ActionResult> GetById(string id)
        {
            var roleVM = await _roleServices.GetRoleById(id);
            if (roleVM == null)
                return NotFound();
            return Ok(roleVM);
        }

        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.UPDATE)]
        public async Task<ActionResult> PutTole(string id, [FromBody] RoleVM roleVM)
        {
            if (id != roleVM.Id.ToString())
                return BadRequest();
            var role = await _roleServices.FindById(id);
            if (role == null)
                return NotFound();
            var rel = await _roleServices.UpdateRole(role, roleVM);
            if (rel.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(rel.Errors);
        }

        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.DELETE)]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await _roleServices.FindById(id);
            if (role == null)
                return NotFound();
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
            return BadRequest(rel.Errors);
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
            _context.Permissions.AddRange(newPermissions);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}