using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrders.Data.Dtos.Pizzas;
using PizzaOrders.Data.Repositories;
using System.Drawing;

namespace PizzaOrders.Controllers
{
    [ApiController]
    [Route("api/pizza/{pizzaId}/SelectedTopping")]
    [AllowAnonymous]
    public class SelectedToppingController : ControllerBase
    {
        private readonly ISelectedToppingRepository _SelectedToppingRepository;

        public SelectedToppingController(ISelectedToppingRepository SelectedToppingRepository)
        {
            _SelectedToppingRepository = SelectedToppingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectedToppingDto>>> GetManyByPizza(Guid pizzaId)
        {
            var SelectedToppings = await _SelectedToppingRepository.GetManyByPizzaAsync(pizzaId);
            return SelectedToppings.Select(o => new SelectedToppingDto(o.ToppingId)).ToList();
        }
    }
}
