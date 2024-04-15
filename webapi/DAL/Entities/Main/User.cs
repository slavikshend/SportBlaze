using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.MN;
using webapi.DAL.Entities.Support;

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

        [Column("salt")]
        public string? Salt { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("role_id")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        [Column("address")]
        public string? Address { get; set; }
        public ICollection<Favourites> Favourites { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
