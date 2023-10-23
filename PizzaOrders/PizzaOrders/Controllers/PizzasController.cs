using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrders.Data.Dtos.Pizzas;
using PizzaOrders.Data.Entities;
using PizzaOrders.Data.Repositories;

namespace PizzaOrders.Controllers
{
    [ApiController]
    [Route("api/pizza")]
    [AllowAnonymous]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaRepository _pizzasRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly ISelectedToppingRepository _selectedToppingRepository;
        private readonly IToppingRepository _toppingRepository;
        public PizzasController(IPizzaRepository pizzasRepository, ISizeRepository sizeRepository,
            ISelectedToppingRepository selectedtoppingRepository, IToppingRepository toppingRepository)
        {
            _pizzasRepository = pizzasRepository;
            _sizeRepository = sizeRepository;
            _selectedToppingRepository = selectedtoppingRepository;
            _toppingRepository = toppingRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecondPizzaDto>>> GetMany()
        {
            var pizzas = await _pizzasRepository.GetManyAsync();
            var Pizzas =new List<SecondPizzaDto>();
            foreach (var item in pizzas)
            {
                var toppingNameList = new List<string>();
                var selectedToppings = await _selectedToppingRepository.GetManyByPizzaAsync(item.Id);
                foreach (var selectedtopping in selectedToppings) 
                {
                    var topping = await _toppingRepository.GetAsync(selectedtopping.ToppingId);
                    if (topping != null)
                    {
                        toppingNameList.Add(topping.Name);
                    }
                }
                var pizzaSize = await _sizeRepository.GetAsync(item.SizeId);


                var newpizza = new SecondPizzaDto(pizzaSize.Name, toppingNameList, item.Price);

                Pizzas.Add(newpizza);
            }
            return Pizzas;
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
            var size = await _sizeRepository.GetAsync(createPizzaDto.Size);
            if (size == null)
            {
                return NotFound();
            }

            var pizza = new Pizza
            {
                SizeId = createPizzaDto.Size,
                Price = createPizzaDto.Price
            };

            await _pizzasRepository.CreateAsync(pizza);

            if (createPizzaDto.Toppings != null)
            {
                foreach (var item in createPizzaDto.Toppings)
                {
                    var topping = await _toppingRepository.GetAsync(item);
                    if (topping != null)
                    {
                        var selectedTopping = new SelectedTopping
                        {
                            PizzaId = pizza.Id,
                            ToppingId = topping.Id
                        };
                        await _selectedToppingRepository.CreateAsync(selectedTopping);
                    }
                }
            }

            return Created("", new PizzaDto(pizza.Id, pizza.SizeId, pizza.Price));
        }

        [HttpPut]
        [Route("calculatePrice")]
        public async Task<ActionResult<double>> CalculatePrice(CalculatePricePizzaDto pizza) 
        {
            try
            {
                var size = await _sizeRepository.GetAsync(pizza.Size);
                if (size == null)
                {
                    return NotFound();
                }
                double price = size.Price;
                if (pizza.Toppings != null)
                {
                    foreach (var item in pizza.Toppings)
                    {
                        var topping = await _toppingRepository.GetAsync (item);
                        if (topping == null)
                        {
                            return NotFound();
                        }
                    }
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
