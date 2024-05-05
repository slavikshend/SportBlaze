using System.Drawing.Design;

namespace webapi.BLL.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; } 
        public string CategoryImageUrl { get; set; }
    }
}
