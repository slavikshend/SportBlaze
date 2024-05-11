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

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
