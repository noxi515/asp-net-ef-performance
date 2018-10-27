using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NX.Data.Entities
{
    [Serializable]
    public class User
    {
        [Key] public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTimeOffset LastLogin { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
