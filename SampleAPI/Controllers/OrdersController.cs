using Microsoft.AspNetCore.Mvc;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Utility;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [TypeFilter(typeof(ExceptionHandler))]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
 
        [HttpGet("[action]")] 
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetRecentOrders();
            return Ok(orders);
        }

        [HttpGet("[action]/{days}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByDays(int days = 0)
        {
            var orders = await _orderRepository.GetRecentOrders(days);
            return Ok(orders);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> SaveOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var orderId = await _orderRepository.AddNewOrder(order);
            return orderId;
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> DeleteOrder(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NoContent();
            }
            var orderId = await _orderRepository.DeleteOrder(id);
            return orderId;
        }

    }
}
