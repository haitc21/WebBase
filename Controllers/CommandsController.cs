using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebBase.Data;
using WebBase.Helpers.TestOPP;
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
            DongVat cho1 = new Cho();
            int cho1Cao = cho1.Cao;
            int cho1Nang = cho1.Nang;
            cho1.Keu();

            Cho cho2 = new Cho(15,20);
            int cho2Cao = cho2.Cao;
            int cho2Nang = cho2.Nang;
            cho2.Keu();

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