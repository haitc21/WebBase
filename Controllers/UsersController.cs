using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebBase.Helpers;
using WebBase.Helpers.Authorization;
using WebBase.Models.RequestModels;
using WebBase.Models.ViewModels;
using WebBase.Services.Interfaces;
using static WebBase.Common.Enums;

namespace WebBase.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PostUser(UserCreateModel request)
        {
            _logger.LogInformation("Begin PostUser API");

            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse("Model invalid!"));
            string id = Guid.NewGuid().ToString();
            var result = await _userService.CreateUser(request, id);
            if (result.Succeeded)
            {
                _logger.LogInformation("End PostUser API - Success");
                return CreatedAtAction(nameof(GetById), new { id = id }, request);
            }
            else
            {
                _logger.LogInformation("End PostUser API - Failed");
                return BadRequest(new ApiBadRequestResponse(result));
            }
        }

        // GET: api/<UsersController>
        [HttpGet]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.VIEW)]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.VIEW)]
        public async Task<ActionResult> GetById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"User id {id} not exsited!"));
            return Ok(user);
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.VIEW)]
        public async Task<ActionResult> GetUser(string filter, int pageIndex = 1, int pageSize = 10)
        {
            var pagination = await _userService.GetUserPagging(filter, pageIndex, pageSize);
            if (pagination.totalRecords == 0)
                return NotFound(new ApiNotFoundResponse($"Can't found user with filter: {filter}!"));
            return Ok(pagination);
        }

        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.UPDATE)]
        [ApiValidationFilter]
        public async Task<ActionResult> PutUser(string id, [FromBody] UserUpdateModel userUM)
        {
            if (id != userUM.Id.ToString())
                return BadRequest(new ApiBadRequestResponse("User id from link can't diffirent user id from body request!"));
            var user = await _userService.FindById(id);
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"User id {id} not exsited!"));
            var rel = await _userService.UpdateUser(user, userUM);
            if (rel.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse(rel));
        }

        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.DELETE)]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userService.FindById(id);
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"User id {id} not exsited!"));
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
            return BadRequest(new ApiBadRequestResponse(rel));
        }

        [HttpGet("{userId}/menu")]
        public async Task<IActionResult> GetMenuByUserPermission(string userId)
        {
            var data = await _userService.GetMenu(userId);
            return Ok(data);
        }

        [HttpPut("{userId}/change-password")]
        [ApiValidationFilter]
        public async Task<IActionResult> PutUserPassword(string userId, [FromBody] UserChangePasswordModel userCPM)
        {
            var user = await _userService.FindById(userId);
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"User id {userId} not exsited!"));
            var result = await _userService.ChangePassword(user, userCPM);
            if (result.Succeeded)
                return NoContent();
            return BadRequest(new ApiBadRequestResponse(result));
        }
    }
}