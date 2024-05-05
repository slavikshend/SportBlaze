using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DAL.Entities.Main
{
    [Table("categories")]
    public class Category : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("image")]
        public string Image { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
}
