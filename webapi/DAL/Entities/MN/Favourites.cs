using webapi.DAL.Entities.Main;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.MN
{
    [Table("favourites")]
    public class Favourites : Entity
    {
        [ForeignKey("User")]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
