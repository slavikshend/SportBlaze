namespace webapi.BLL.Models
{
    public class SimplifiedProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Rating { get; set; }
        public bool IsFavourite { get; set;} 
    }
}
