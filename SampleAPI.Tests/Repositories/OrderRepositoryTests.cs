using FluentAssertions;
using Moq;
using NUnit.Framework;
using SampleAPI.Entities;
using SampleAPI.Repositories;

namespace SampleAPI.Tests.Repositories
{
    [TestFixture]
    public class OrderRepositoryTests
    {

        private Mock<IOrderRepository> _orderRepository;

        [SetUp]
        public void Init()
        {
            _orderRepository = new Mock<IOrderRepository>();
        }

        [Test]
        public void TestAddOrder()
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
                result = _orderRepository.Setup(serv => serv.AddNewOrder(mockData))
                .ReturnsAsync(id).As<Guid>();
            }).GetAwaiter().GetResult();
            Assert.Equals(result, id);
        }
        [Test]
        public void TestRemoveOrder()
        {
            int result = 0;
            Guid id = Guid.NewGuid();
            Task.Run(async () =>
            {
                result = _orderRepository.Setup(serv => serv.DeleteOrder(id))
                .ReturnsAsync(1).As<int>();
            }).GetAwaiter().GetResult();
            Assert.Equals(result, 1);
        }
    }
}