using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using NX.Data;

namespace AspNetOwinApp.Controllers
{
    [RoutePrefix("api/v1/async")]
    public class AsyncController : ApiController
    {
        private readonly IDbContextFactory<DatabaseContext> _factory;

        public AsyncController(IDbContextFactory<DatabaseContext> factory)
        {
            _factory = factory;
        }

        [HttpGet]
        [Route("writable")]
        public async Task<IHttpActionResult> GetAsync()
        {
            using (var context = _factory.Create())
            {
                var result = await context.Users
                    .Select(u => new
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Name = u.Name,
                        LastLogin = u.LastLogin,
                        Roles = u.Roles,
                        Departments = u.Departments
                    })
                    .ToListAsync();
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("readonly")]
        public async Task<IHttpActionResult> GetReadonlyAsync()
        {
            using (var context = _factory.Create())
            {
                var result = await context.Users
                    .AsNoTracking()
                    .Select(u => new
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Name = u.Name,
                        LastLogin = u.LastLogin,
                        Roles = u.Roles,
                        Departments = u.Departments
                    })
                    .ToListAsync();
                return Ok(result);
            }
        }
    }
}
