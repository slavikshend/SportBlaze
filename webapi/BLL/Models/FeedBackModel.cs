namespace webapi.BLL.Models
{
    public class FeedBackModel
    {
        public string? Email { get; set; }
        public int Rating { get; set; }

        public DateTimeOffset Date { get; set; }
        public string? Comment { get; set; }
    }
}
