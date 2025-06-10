using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using webapi.BLL.Controllers;
using webapi.BLL.Models;
using webapi.BLL.Services.Interfaces;
using Xunit;

namespace SportBlazeTest
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task GetPaymentMethods_ReturnsOkWithPaymentMethods()
        {
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(service => service.GetPaymentMethods())
                            .ReturnsAsync(new List<PaymentModel>
                            {
                                new PaymentModel { Id = 1, Name = "PaymentMethod1" },
                                new PaymentModel { Id = 2, Name = "PaymentMethod2" }
                            });
            var controller = new OrderController(mockOrderService.Object);
            var result = await controller.GetPaymentMethods();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var paymentMethods = Assert.IsAssignableFrom<IEnumerable<PaymentModel>>(okResult.Value);
            Assert.Equal(2, paymentMethods.Count());
        }

        [Fact]
        public async Task GetDeliveryMethods_ReturnsOkWithDeliveryMethods()
        {
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(service => service.GetDeliveryMethods())
                            .ReturnsAsync(new List<DeliveryModel>
                            {
                                new DeliveryModel { Id = 1, Name = "DeliveryMethod1" },
                                new DeliveryModel { Id = 2, Name = "DeliveryMethod2" }
                            });
            var controller = new OrderController(mockOrderService.Object);
            var result = await controller.GetDeliveryMethods();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deliveryMethods = Assert.IsAssignableFrom<IEnumerable<DeliveryModel>>(okResult.Value);
            Assert.Equal(2, deliveryMethods.Count());
        }

        [Fact]
        public async Task AddOrder_ValidOrderRequest_ReturnsOkWithOrderId()
        {
            var mockOrderService = new Mock<IOrderService>();
            var orderRequest = new OrderRequestModel();
            mockOrderService.Setup(service => service.AddOrder(orderRequest))
                            .ReturnsAsync(1);
            var controller = new OrderController(mockOrderService.Object);
            var result = await controller.AddOrder(orderRequest);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var orderId = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, orderId);
        }

        [Fact]
        public async Task AddOrder_InvalidOrderRequest_ReturnsBadRequest()
        {
            var mockOrderService = new Mock<IOrderService>();
            var orderRequest = new OrderRequestModel();
            var controller = new OrderController(mockOrderService.Object);
            controller.ModelState.AddModelError("PropertyName", "Error Message");
            var result = await controller.AddOrder(orderRequest);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddPayment_ValidOrderId_ReturnsOk()
        {
            var mockOrderService = new Mock<IOrderService>();
            var orderId = 1;
            mockOrderService.Setup(service => service.AddPayment(orderId))
                            .ReturnsAsync(true);
            var controller = new OrderController(mockOrderService.Object);
            var result = await controller.AddPayment(orderId);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddPayment_InvalidOrderId_ReturnsNotFound()
        {
            var mockOrderService = new Mock<IOrderService>();
            var orderId = 1;
            mockOrderService.Setup(service => service.AddPayment(orderId))
                            .ReturnsAsync(false);
            var controller = new OrderController(mockOrderService.Object);
            var result = await controller.AddPayment(orderId);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
