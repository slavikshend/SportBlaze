using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.MN;

namespace webapi.DAL.Entities.Support
{
    [Table("payment_methods")]
    public class PaymentMethod : Entity
    {
        [Column("name")]
        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
