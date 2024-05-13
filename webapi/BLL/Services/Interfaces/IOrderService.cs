using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.DAL.Entities.Support;

namespace webapi.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<PaymentModel>> GetPaymentMethods();
        Task<IEnumerable<DeliveryModel>> GetDeliveryMethods();
        Task<int> AddOrder(OrderRequestModel orderRequest);
        Task<bool> AddPayment(int orderId);
        Task<IEnumerable<OrderModel>> GetAllOrders();
        Task<bool> ChangeOrderStatus(int orderId, int statusId);
    }
}
