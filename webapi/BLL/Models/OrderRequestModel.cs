namespace webapi.BLL.Models
{
    public class OrderRequestModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PaymentMethodId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string OrderAddress { get; set; }
        public decimal Total { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}
