using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Support
{
    [Table("delivery_methods")]
    public class DeliveryMethod : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
