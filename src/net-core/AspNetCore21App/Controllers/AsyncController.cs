using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NX.Data;

namespace AspNetCore20App.Controllers
{
    [Route("api/v1/[Controller]")]
    public class AsyncController : Controller
    {
        [HttpGet]
        [Route("writable")]
        public async Task<IActionResult> GetAsync([FromServices] DatabaseContext context)
        {
            var result = await context.Users
                .Include(u => u.Roles)
                .Include(u => u.Departments)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("readonly")]
        public async Task<IActionResult> GetReadonlyAsync([FromServices] DatabaseContext context)
        {
            var result = await context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .Include(u => u.Departments)
                .ToListAsync();
            return Ok(result);
        }
    }
}