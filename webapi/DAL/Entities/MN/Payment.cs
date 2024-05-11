using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.Support;

namespace webapi.DAL.Entities.MN
{
    [Table("payments")]
    public class Payment : Entity
    {
        [Column("order_id")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Column("payment_method_id")]
        [Required]
        [ForeignKey("PaymentMethod")]
        public int PaymentMethodId { get; set; }

        [Column("payment_date")]
        public DateTimeOffset PaymentDate { get; set; }

        [Column("is_completed")]
        public bool isCompleted { get; set; }

        public Order Order { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
