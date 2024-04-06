using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.MN;

namespace webapi.DAL.Entities.Support
{
    [Table("roles")]
    public class Role : Entity
    {
        [Column("name")]
        public string Name { get; set; }
        public ICollection<RoleAssignment> RoleAssignments { get; set; }
    }
}
