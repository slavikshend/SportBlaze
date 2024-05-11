namespace webapi.BLL.Models
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Brand { get; set; }
        public string SubCategory { get; set; }
        public List<FeedBackModel> Feedbacks { get; set; }
        public List<FeatureModel> Features { get; set; }
    }
}
