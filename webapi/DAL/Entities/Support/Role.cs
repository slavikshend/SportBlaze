using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.Main;

namespace webapi.DAL.Entities.Support
{
    [Table("roles")]
    public class Role : Entity
    {
        [Column("name")]
        public string Name { get; set; }
        public ICollection<User> Users;
    }
}
