using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrders.Data.Dtos.Orders;
using PizzaOrders.Data.Dtos.Pizzas;
using PizzaOrders.Data.Entities;
using PizzaOrders.Data.Repositories;

namespace PizzaOrders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [AllowAnonymous]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPizzaRepository _pizzaRepository;
        public OrdersController(IOrderRepository orderRepository, IPizzaRepository pizzaRepository)
        {
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
        }

        [HttpPost]
        [Route("{pizzaId}")]
        public async Task<ActionResult<CreateOrderDto>> Create(Guid pizzaId)
        {
            var pizza = await _pizzaRepository.GetAsync(pizzaId);
            if (pizza == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                PizzaId = pizza.Id
            };

            await _orderRepository.CreateAsync(order);

            return Ok(new OrderDto(order.Id, order.PizzaId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMany()
        {
            var orders = await _orderRepository.GetManyAsync();
            return orders.Select(o => new OrderDto(o.Id, o.PizzaId)).ToList();
        }
    }
}
