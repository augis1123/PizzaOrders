using Microsoft.AspNetCore.Mvc;
using PizzaOrders.Data.Dtos.Pizzas;
using PizzaOrders.Data.Entities;
using PizzaOrders.Data.Repositories;

namespace PizzaOrders.Controllers
{
    [ApiController]
    [Route("api/pizza")]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzassRepository _pizzasRepository;
        public PizzasController(IPizzassRepository pizzasRepository)
        {
            _pizzasRepository = pizzasRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaDto>>> GetMany()
        {
            var pizzas = await _pizzasRepository.GetManyAsync();
            return pizzas.Select(o => new PizzaDto(o.Id, o.Size, o.Price)).ToList();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var pizza = await _pizzasRepository.GetAsync(id);
            if (pizza == null) 
            {
                return NotFound(); 
            }

            await _pizzasRepository.DeleteAsync(pizza);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PizzaDto>> Create(CreatePizzaDto createPizzaDto)
        {
            var pizza = new Pizza
            {
                Size = createPizzaDto.Size,
                Price = createPizzaDto.Price
            };

            await _pizzasRepository.CreateAsync(pizza);

            return Created("", new PizzaDto(pizza.Id, pizza.Size, pizza.Price));
        }

        [HttpPut]
        public async Task<ActionResult<double>> CalculatePrice(CalculatePricePizzaDto pizza) 
        {
            try
            {
                double price = (int)pizza.Size;
                if (pizza.Toppings != null)
                {
                    price += pizza.Toppings.Count;
                    if (pizza.Toppings.Count >= 3)
                    {
                        price = Math.Round(price * 0.9, 2);
                    }
                }
                return Ok(price);
            }
            catch(Exception ex) 
            {
                return BadRequest();
            }
        }
    }
}
