using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.MN;
using webapi.DAL.Entities.Support;

namespace webapi.DAL.Entities.Main
{
    [Table("products")]
    public class Product : Entity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("image")]
        public string ImageUrl { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("discount")]
        public decimal Discount { get; set; }

        [Column("brand_id")]
        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        [Column("subcategory_id")]
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public Brand Brand { get; set; }
        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<Favourites> Favourites { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
