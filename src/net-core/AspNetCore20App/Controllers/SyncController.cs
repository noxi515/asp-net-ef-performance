using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NX.Data;

namespace AspNetCore20App.Controllers
{
    [Route("api/v1/[Controller]")]
    public class SyncController : Controller
    {
        [HttpGet]
        [Route("writable")]
        public IActionResult GetAsync([FromServices] DatabaseContext context)
        {
            var result = context.Users
                .Include(u => u.Roles)
                .Include(u => u.Departments)
                .ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("readonly")]
        public IActionResult GetReadonlyAsync([FromServices] DatabaseContext context)
        {
            var result = context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .Include(u => u.Departments)
                .ToList();
            return Ok(result);
        }
    }
}