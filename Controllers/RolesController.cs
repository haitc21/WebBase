using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Interfaces;

namespace WebBase.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IRoleServices _roleServices;

        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        // URL: POST https://localhost:5000/api/roles
        [HttpPost]
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

        // URL: GET https://localhost:5000/api/roles/
        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            var roleVM = await _roleServices.GetAllRoles();
            if (roleVM == null)
                return NotFound();
            return Ok(roleVM);
        }

        // URL: GET https://localhost:5000/api/roles/?filter={filter}&pageIndex=1&pageSize=10
        [HttpGet("filter")]
        public async Task<ActionResult> GetRole(string filter, int pageIndex = 1, int pageSize = 10)
        {
            var pagination = await _roleServices.GetRolePagging(filter, pageIndex, pageSize);
            if (pagination.totalRecord == 0)
                return NotFound();
            return Ok(pagination);
        }

        // URL: GET https://localhost:5000/api/roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var roleVM = await _roleServices.GetRoleById(id);
            if (roleVM == null)
                return NotFound();
            return Ok(roleVM);
        }

        // URL: PUT https://localhost:5000/api/roles/{id}
        [HttpPut("{id}")]
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

        // URL: DELETE https://localhost:5000/api/roles/{id}
        [HttpDelete("{id}")]
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
    }
}