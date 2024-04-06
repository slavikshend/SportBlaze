using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.Main;

namespace webapi.DAL.Entities.MN
{
    [Table("order_details")]
    public class OrderDetails : Entity
    {
        [ForeignKey("Order")]
        [Column("order_id")]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
