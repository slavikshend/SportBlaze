using System.ComponentModel.DataAnnotations.Schema;
using webapi.DAL.Entities.Main;

namespace webapi.DAL.Entities.MN
{
    [Table("feedbacks")]
    public class FeedBack : Entity
    {
        [ForeignKey("User")]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("date")]
        public DateTimeOffset Date { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
