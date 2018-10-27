using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using NX.Data;

namespace AspNetOwinApp.Controllers
{
    [RoutePrefix("api/v1/sync")]
    public class SyncController : ApiController
    {
        private readonly IDbContextFactory<DatabaseContext> _factory;

        public SyncController(IDbContextFactory<DatabaseContext> factory)
        {
            _factory = factory;
        }

        [HttpGet]
        [Route("writable")]
        public IHttpActionResult GetAsync()
        {
            using (var context = _factory.Create())
            {
                var result = context.Users
                    .Select(u => new
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Name = u.Name,
                        LastLogin = u.LastLogin,
                        Roles = u.Roles,
                        Departments = u.Departments
                    })
                    .ToList();
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("readonly")]
        public IHttpActionResult GetReadonlyAsync()
        {
            using (var context = _factory.Create())
            {
                var result = context.Users
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
                    .ToList();
                return Ok(result);
            }
        }
    }
}