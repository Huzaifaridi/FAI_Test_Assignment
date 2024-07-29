using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SampleApiDbContext _context;
        private readonly ILogger _logger;

        public OrderRepository(SampleApiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<OrderRepository>();
        }
        public async Task<Guid> AddNewOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order.Id;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error in AddNewOrder");
                throw;
            }
        }

        public async Task<List<Order>> GetRecentOrders(int days = 0)
        {
            try
            {
                if (days == 0)
                {
                    return await _context.Orders.Where(p => !p.OrderDeleted).OrderByDescending(p => p.OrderDate).ToListAsync();
                }
                else
                {
                    DateTime endDate = DateTime.Now;
                    DateTime startDate = endDate.AddDays(-days);

                    // Extend endDate by 2 days if it includes a weekend
                    DateTime adjustedEndDate = endDate;
                    if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday ||
                        endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        adjustedEndDate = endDate.AddDays(-2);
                    }

                    // Filter the orders based on the adjusted date range
                    var filteredOrders = _context.Orders
                        .Where(o => o.OrderDate >= adjustedEndDate && o.OrderDate <= endDate)
                        .ToList();
                    return filteredOrders;
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error in GetRecentOrders");
                throw;
            }
        }
        public async Task<int> DeleteOrder(Guid id)
        {
            try
            {
                var item = await _context.Orders.FindAsync(id);
                if (item == null)
                {
                    return 0;
                }

                item.OrderDeleted = true;
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error while Delete Order");
                throw;
            }
        }
    }
}
