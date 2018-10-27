using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NX.Data;
using NX.Data.Entities;

namespace DataGenerator
{
    class Program
    {
        private static readonly Random R = new Random();

        static async Task Main(string[] args)
        {
            var connectionStrings = args[0];
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(connectionStrings)
                .Options;

            using (var context = new DatabaseContext(options))
            {
                await context.Database.EnsureCreatedAsync();

                using (var tx = await context.Database.BeginTransactionAsync())
                {
                    var enumerable = Enumerable.Range(0, 20)
                        .Select(_ => new User
                        {
                            Id = Guid.NewGuid(),
                            Email = "hoge@example.com",
                            Name = "hogehoge",
                            LastLogin = DateTimeOffset.UtcNow,
                            Roles = new List<Role>
                            {
                                new Role
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "role0",
                                    Deleted = R.Next(2) == 0
                                },
                                new Role
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "role1",
                                    Deleted = R.Next(2) == 0
                                }
                            },
                            Departments = new List<Department>
                            {
                                new Department
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "department0",
                                    Deleted = R.Next(2) == 0
                                },
                                new Department
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "department1",
                                    Deleted = R.Next(2) == 0
                                },
                                new Department
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "department2",
                                    Deleted = R.Next(2) == 0
                                }
                            }
                        });

                    await context.Database.ExecuteSqlCommandAsync("DELETE FROM Roles");
                    await context.Database.ExecuteSqlCommandAsync("DELETE FROM Departments");
                    await context.Database.ExecuteSqlCommandAsync("DELETE FROM Users");
                    await context.AddRangeAsync(enumerable);
                    await context.SaveChangesAsync();
                    tx.Commit();
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
