using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrders.Data.Dtos.Pizzas;
using PizzaOrders.Data.Repositories;
using System.Drawing;

namespace PizzaOrders.Controllers
{
    [ApiController]
    [Route("api/size")]
    [AllowAnonymous]
    public class SizeController : ControllerBase
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeController(ISizeRepository sizeRepository) 
        {
            _sizeRepository = sizeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeDto>>> GetMany()
        {
            var sizes = await _sizeRepository.GetManyAsync();
            return sizes.Select(o => new SizeDto(o.Id, o.Name, o.Price)).ToList();
        }
    }
}
