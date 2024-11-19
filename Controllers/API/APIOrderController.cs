using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using shop_app.Models;
using shop_app.Services;

namespace shop_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIOrderController : ControllerBase
    {
        private readonly IServiceOrder _serviceOrder;

        public APIOrderController (IServiceOrder serviceOrder)
        {
            _serviceOrder = serviceOrder;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _ = await _serviceOrder.CreateAsync(order);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _serviceOrder.ReadAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _serviceOrder.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            var updatedOrder = await _serviceOrder.UpdateAsync(id, order);
            if (updatedOrder == null)
                return BadRequest("Invalid order data or ID mismatch.");

            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _serviceOrder.DeleteAsync(id);
            if (!result)
                return NotFound($"Order with ID {id} not found.");

            return NoContent();
        }
    }
}
