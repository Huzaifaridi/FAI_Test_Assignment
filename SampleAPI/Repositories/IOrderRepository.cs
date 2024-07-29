using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Guid> AddNewOrder(Order order);
        Task<List<Order>> GetRecentOrders(int days = 0);
        Task<int> DeleteOrder(Guid id);
    }
}
