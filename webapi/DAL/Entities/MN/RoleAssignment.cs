using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.Support;

namespace webapi.DAL.Entities.MN
{
    [Table("role_assignment")]
    public class RoleAssignment : Entity
    {
        [Column("role_id")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("User")]
        [Column("user_email")]
        public string UserEmail { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
