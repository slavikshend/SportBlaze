using webapi.DAL.Entities.Main;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Support
{
    [Table("features")]
    public class Feature : Entity
    {
        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("value")]
        public string Value { get; set; }

        public Product Product { get; set; }
    }
}
