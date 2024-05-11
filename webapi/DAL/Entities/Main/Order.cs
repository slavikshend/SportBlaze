using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.Support;
using webapi.DAL.Entities.MN;

namespace webapi.DAL.Entities.Main
{
    [Table("orders")]
    public class Order : Entity
    {
        [ForeignKey("User")]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [Column("payment_id")]
        [Required]
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }

        [Column("order_date")]
        [Required]
        public DateTimeOffset OrderDate { get; set; }

        [Column("delivery_method_id")]
        [Required]
        [ForeignKey("DeliveryMethod")]
        public int DeliveryMethodId { get; set; }

        [Column("delivery_address")]
        [Required]
        public string DeliveryAddress { get; set; }

        [Column("order_status_id")]
        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; }

        [Column("total")]
        [Required]
        public decimal Total { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Payment Payment { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}
