using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Support
{
    [Table("payment_methods")]
    public class PaymentMethod : Entity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
