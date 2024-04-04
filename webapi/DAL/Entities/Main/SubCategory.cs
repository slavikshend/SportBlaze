using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Main
{
    [Table("subcategories")]
    public class SubCategory : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("image")]
        public string ImageUrl { get; set; }

        [Column("category_id")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
