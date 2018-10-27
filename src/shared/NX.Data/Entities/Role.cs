using System;
using System.ComponentModel.DataAnnotations;

namespace NX.Data.Entities
{
    [Serializable]
    public class Role
    {
        [Key] public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
