using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using NX.Data;

namespace AspNetOwinApp
{
    public class DatabaseContextFactory : IDbContextFactory<DatabaseContext>
    {
        private readonly string _connectionStrings;

        public DatabaseContextFactory()
        {
            _connectionStrings = ConfigurationManager.ConnectionStrings["sample"].ConnectionString ??
                                 throw new InvalidOperationException("ConnectionStrings not defined.");
        }

        public DatabaseContext Create()
        {
            return new DatabaseContext(_connectionStrings);
        }
    }
}