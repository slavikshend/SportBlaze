using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.MN;

namespace webapi.DAL.Entities.Main
{
    [Table("users")]
    public class User 
    {
        [Key]
        [Column("email")]
        [Required]
        public string Email { get; set; }

        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("hashed_password")]
        public string? HashedPassword { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("password")]
        public string? City { get; set; }

        [Column("address")]
        public string? Address { get; set; }
        public ICollection<Favourites> Favourites { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<RoleAssignment> RoleAssignments { get; set; }
    }
}
