using Microsoft.EntityFrameworkCore;
using shop_app.Models;

namespace shop_app.Services
{
    public interface IServiceOrder
    {
        Task<Order?> CreateAsync(Order? product);
        Task<IEnumerable<Order>> ReadAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> UpdateAsync(int id, Order? product);
        Task<bool> DeleteAsync(int id);
    }
    public class ServiceOrder : IServiceOrder
    {
        private readonly OrderContext _orderContext;
        private readonly ILogger<ServiceProduct> _logger;
        public ServiceOrder(
            OrderContext orderContext,
            ILogger<ServiceProduct> logger)
        {
            _orderContext = orderContext;
            _logger = logger;
        }

        public async Task<Order?> CreateAsync(Order? order)
        {
            if (order == null)
            {
                _logger.LogWarning("Attempt created a product with null");
                return null;
            }
            await _orderContext.AddAsync(order);
            await _orderContext.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _orderContext.Orders.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _orderContext.Orders.Remove(product);
            await _orderContext.SaveChangesAsync();
            return true;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _orderContext.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> ReadAsync()
        {
            return await _orderContext.Orders.ToListAsync();
        }

        public async Task<Order?> UpdateAsync(int id, Order? order)
        {
            if (order == null || id != order.Id)
            {
                _logger.LogWarning($"{nameof(Order)}: {id}");
                return null;
            }
            try
            {
                _orderContext.Orders.Update(order);
                await _orderContext.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
