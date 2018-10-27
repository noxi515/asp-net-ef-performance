#if NETFRAMEWORK

using System.Data.Entity;
using NX.Data.Entities;

namespace NX.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DatabaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}

#else

using Microsoft.EntityFrameworkCore;
using NX.Data.Entities;

namespace NX.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}

#endif
