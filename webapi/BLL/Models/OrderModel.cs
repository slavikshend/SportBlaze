namespace webapi.BLL.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryName { get; set; }
        public string PaymentName { get; set; }
        public string Status { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public bool IsPaymentSuccessfull { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}
