using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Data;
using WebBase.Data.Entities;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Interfaces;

namespace WebBase.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ApplicationDbContext context,
            IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserCreateModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            string id = Guid.NewGuid().ToString();
            var result = await _userService.CreateUser(request, id);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, request);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetUser(string filter, int pageIndex = 1, int pageSize = 10)
        {
            var pagination = await _userService.GetUserPagging(filter, pageIndex, pageSize);
            if (pagination.totalRecord == 0)
                return NotFound();
            return Ok(pagination);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(string id, [FromBody] UserUpdateModel userUM)
        {
            if (id != userUM.Id.ToString())
                return BadRequest();
            var user = await _userService.FindById(id);
            if (user == null)
                return NotFound();
            var rel = await _userService.UpdateUser(user, userUM);
            if (rel.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(rel.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userService.FindById(id);
            if (user == null)
                return NotFound();
            var userVM = new UserVM()
            {
                UserName = user.UserName,
                Email = user.Email,
                Dob = user.Dob.ToString("dd/MM/yyyy"),
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            var rel = await _userService.DeleteUser(user);
            if (rel.Succeeded)
            {
                return Ok(userVM);
            }
            return BadRequest(rel.Errors);
        }

        [HttpGet("{userId}/menu")]
        public async Task<IActionResult> GetMenuByUserPermission(string userId)
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
            return Ok(data);
        }
    }
}