using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrders.Data.Dtos.Pizzas;
using PizzaOrders.Data.Repositories;
using System.Drawing;

namespace PizzaOrders.Controllers
{
    [ApiController]
    [Route("api/Topping")]
    [AllowAnonymous]
    public class ToppingController : ControllerBase
    {
        private readonly IToppingRepository _toppingRepository;

        public ToppingController(IToppingRepository ToppingRepository)
        {
            _toppingRepository = ToppingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingDto>>> GetMany()
        {
            var toppings = await _toppingRepository.GetManyAsync();
            return toppings.Select(o => new ToppingDto(o.Id, o.Name)).ToList();
        }
    }
}
