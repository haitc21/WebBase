using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Interfaces;

namespace WebBase.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
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
    }
}