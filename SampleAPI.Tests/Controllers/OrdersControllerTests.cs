using Moq;
using NUnit.Framework;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;

namespace SampleAPI.Tests.Controllers
{
    [TestFixture]
    public class OrdersControllerTests
    {
        Mock<IOrderRepository> _orderRepository;
        OrdersController _ordersController;

        [SetUp]
        public void Init()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _ordersController = new OrdersController(_orderRepository.Object);
        }

        [Test]
        public void TestSaveOrderAction()
        {
            Guid result = Guid.Empty;
            Guid id = Guid.NewGuid();
            var mockData = new Order()
            {
                Name = "Cake",
                Desscription = "Chocolate Cake",
                OrderDate = DateTime.Now,
            };
            Task.Run(async () =>
            {
                _orderRepository.Setup(serv => serv.AddNewOrder(mockData))
                .ReturnsAsync(id);
                result = _ordersController.SaveOrder(mockData).Result.Value;
            }).GetAwaiter().GetResult();
            Assert.Equals(id, result);
        }
        [Test]
        public void TestRemoveOrder()
        {
            int result = 0;
            Guid id = Guid.NewGuid();
            Task.Run(async () =>
            {
                _orderRepository.Setup(serv => serv.DeleteOrder(id))
                .ReturnsAsync(1);
                result = _ordersController.DeleteOrder(id).Result.Value;
            }).GetAwaiter().GetResult();
            Assert.Equals(result, 1);
        }

    }
}
