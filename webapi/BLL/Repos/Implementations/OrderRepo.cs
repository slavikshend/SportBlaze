using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.MN;
using webapi.DAL.Entities.Support;

namespace webapi.BLL.Repos.Implementations
{
    public class OrderRepo : IOrderRepo
    {
        private readonly SportsShopDbContext _context;

        public OrderRepo(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
            
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _context.DeliveryMethods.ToListAsync();
        }

        public async Task AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order: {ex.Message}");
                return 0;
            }
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.Payment) 
                    .Include(o => o.OrderDetails) 
                    .FirstOrDefaultAsync(o => o.Id == orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting order by ID: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateOrder(Order order)
        {
            try
            {
                var existingOrder = await _context.Orders.FindAsync(order.Id);

                if (existingOrder == null)
                {
                    throw new Exception($"Order with ID {order.Id} not found");
                }

                existingOrder.Payment.isCompleted = order.Payment.isCompleted;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderDetails)
                    .Include(o => o.User)
                    .Include(o => o.DeliveryMethod)
                    .Include(o => o.Payment).ThenInclude(p => p.PaymentMethod)
                    .Include(o => o.OrderStatus)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                throw;
            }
        }
    }
}
