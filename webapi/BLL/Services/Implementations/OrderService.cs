using System.Collections.Generic;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.MN;
using webapi.DAL.Entities.Support;

namespace webapi.BLL.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IUserRepo _userRepo;

        public OrderService(IOrderRepo orderRepository, IUserRepo userRepo)
        {
            _orderRepo = orderRepository;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<PaymentModel>> GetPaymentMethods()
        {
            IEnumerable<PaymentMethod> methods = await _orderRepo.GetPaymentMethodsAsync();
            List<PaymentModel> methods2 = new List<PaymentModel>();
            foreach (var method in methods)
            {
                var paymentMethod = new PaymentModel
                {
                    Id = method.Id,
                    Name = method.Name
                };
                methods2.Add(paymentMethod);
            }
            return methods2;
        }

        public async Task<IEnumerable<DeliveryModel>> GetDeliveryMethods()
        {
            IEnumerable<DeliveryMethod> methods = await _orderRepo.GetDeliveryMethodsAsync();
            List<DeliveryModel> methods2 = new List<DeliveryModel>();
            foreach (var method in methods)
            {
                var deliveryMethod = new DeliveryModel
                {
                    Id = method.Id,
                    Name = method.Name
                };
                methods2.Add(deliveryMethod); 
            }
            return methods2;
        }

        public async Task<int> AddOrder(OrderRequestModel orderRequest)
        {
            try
            {
                User existingUser = await _userRepo.GetRegisteredUser(orderRequest.Email);
                if (existingUser == null)
                {
                    User user = new User
                    {
                        Email = orderRequest.Email,
                        FirstName = orderRequest.FirstName,
                        LastName = orderRequest.LastName,
                        Phone = orderRequest.Phone,
                        Address = orderRequest.Address,
                        City = orderRequest.City,
                        RoleId = 3
                    };
                    await _userRepo.AddUser(user);
                }

                Payment payment = new Payment
                {
                    PaymentMethodId = orderRequest.PaymentMethodId,
                    PaymentDate = DateTime.Now,
                    isCompleted = false
                };

                Order order = new Order
                {
                    UserEmail = orderRequest.Email,
                    OrderDate = orderRequest.OrderDate,
                    DeliveryMethodId = orderRequest.DeliveryMethodId,
                    DeliveryAddress = orderRequest.OrderAddress,
                    OrderStatusId = orderRequest.OrderStatusId,
                    Total = orderRequest.Total,
                    Payment = payment,
                    OrderDetails = new List<OrderDetails>()
                };

                foreach (var cartItem in orderRequest.CartItems)
                {
                    OrderDetails orderDetail = new OrderDetails
                    {
                        ProductId = cartItem.Id,
                        Quantity = cartItem.Quantity
                    };
                    order.OrderDetails.Add(orderDetail);
                }

                return await _orderRepo.AddOrder(order); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order: {ex.Message}");
                return 0;
            }
        }

        public async Task<bool> AddPayment(int orderId)
        {
            try
            {
                Order order = await _orderRepo.GetOrderById(orderId);

                if (order == null)
                {
                    return false;
                }
                order.Payment.isCompleted = true;
                await _orderRepo.UpdateOrder(order);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding payment: {ex.Message}");
                return false;
            }
        }

    }
}
