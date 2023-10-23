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
        private readonly ISelectedToppingRepository _selectedToppingRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IToppingRepository _toppingRepository;

        public OrdersController(IOrderRepository orderRepository, IPizzaRepository pizzaRepository,
            ISelectedToppingRepository selectedToppingRepository, IToppingRepository toppingRepository, ISizeRepository sizeRepository)
        {
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
            _selectedToppingRepository = selectedToppingRepository;
            _sizeRepository = sizeRepository;
            _toppingRepository = toppingRepository;
        }

        [HttpPost]
        [Route("{pizzaId}")]
        public async Task<ActionResult<CreateOrderDto>> Create(Guid pizzaId)
        {
            double price = 0;
            var pizza = await _pizzaRepository.GetAsync(pizzaId);
            if (true)
            {
                try
                {
                    var size = await _sizeRepository.GetAsync(pizza.SizeId);
                    if (size == null)
                    {
                        return NotFound();
                    }
                    price = size.Price;
                    var toppings = await _selectedToppingRepository.GetManyByPizzaAsync(pizza.Id);
                    if (toppings != null)
                    {
                        foreach (var item in toppings)
                        {
                            var topping = await _toppingRepository.GetAsync(item.ToppingId);
                            if (topping == null)
                            {
                                return NotFound();
                            }
                        }
                        price += toppings.Count;
                        if (toppings.Count >= 3)
                        {
                            price = Math.Round(price * 0.9, 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            if (pizza == null)
            {
                return NotFound();
            }
            pizza.Price = price;
            var order = new Order
            {
                PizzaId = pizza.Id
            };

            await _orderRepository.CreateAsync(order);

            return Ok(new OrderDto(order.Id, order.PizzaId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecondOrderDto>>> GetMany()
        {
            var orders = await _orderRepository.GetManyAsync();
            var ordersDto = new List<SecondOrderDto>();
            foreach (var order in orders)
            {
                var pizza = await _pizzaRepository.GetAsync(order.PizzaId);
                var toppingNameList = new List<string>();
                var selectedToppings = await _selectedToppingRepository.GetManyByPizzaAsync(pizza.Id);
                foreach (var select in selectedToppings)
                {
                    var topping = await _toppingRepository.GetAsync(select.ToppingId);
                    if (topping != null)
                    {
                        toppingNameList.Add(topping.Name);
                    }
                }
                var pizzaSize = await _sizeRepository.GetAsync(pizza.SizeId);


                var newpizza = new SecondOrderDto(pizzaSize.Name, toppingNameList, pizza.Price);

                ordersDto.Add(newpizza);
            }
            return ordersDto;
        }
    }
}
