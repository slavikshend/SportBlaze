using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Services;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Support;

namespace webapi.BLL.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("payment-methods")]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetPaymentMethods()
        {
            var paymentMethods = await _orderService.GetPaymentMethods();
            foreach(var method in paymentMethods)
            {
                Console.WriteLine(method.Name);
            }
            return Ok(paymentMethods);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IEnumerable<DeliveryModel>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethods();
            return Ok(deliveryMethods);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderRequestModel orderRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int orderId = await _orderService.AddOrder(orderRequest);
                    return Ok(orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while adding the order");
            }
        }

        [HttpPost("{orderId}/payment")]
        public async Task<IActionResult> AddPayment(int orderId)
        {
            try
            {
                bool IsSuccessful = await _orderService.AddPayment(orderId);
                if (!IsSuccessful)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding payment: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while adding the payment");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,RegisteredUser")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving orders: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while retrieving orders");
            }
        }
        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, [FromBody] int statusId)
        {
            try
            {
                bool success = await _orderService.ChangeOrderStatus(orderId, statusId);
                if (!success)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing order status: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while changing the order status");
            }
        }
    }
}
