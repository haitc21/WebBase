using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Data;
using WebBase.Models.ViewModels;

namespace WebBase.Controllers
{
    public class CommandsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CommandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCommants()
        {
            var commands = _context.Commands;

            var commandVms = await commands.Select(u => new CommandVM()
            {
                Id = u.Id,
                Name = u.Name,
            }).ToListAsync();

            return Ok(commandVms);
        }
    }
}