using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Main
{
    [Table("brands")]
    public class Brand : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("image")]
        public string Image { get; set; }

        public List<Product> Products { get; set; }
    }
}
