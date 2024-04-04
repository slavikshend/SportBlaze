using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Support
{
    [Table("order_status")]
    public class OrderStatus : Entity
    {
        [Column("name")]
        [Required]
        public string Name { get; set; }
    }
}
