using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.MN;
using webapi.DAL.Entities.Support;

namespace webapi.BLL.Repos.Interfaces
{
    public interface IOrderRepo
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<int> AddOrder(Order order);
        Task AddPayment(Payment payment);
        Task<Order> GetOrderById(int orderId);
        Task UpdateOrder(Order order);
    }
}
