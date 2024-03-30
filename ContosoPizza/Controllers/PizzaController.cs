using ContosoPizza.Configuration;
using ContosoPizza.Data;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{

    [ApiController]
    [Route("Pizza")]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        
        private readonly IPizzaService _pizzaService;

        public IConfiguration configuration { get; }

        public PizzaController(ILogger<PizzaController> logger, IPizzaService pizzaService, IConfiguration myConfiguration) {
            _logger = logger;
            _pizzaService = pizzaService;
            configuration = myConfiguration;
            _logger.LogDebug("nlog is integrated to pizza controller");
        }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAllPizzas()
        {
            _logger.LogInformation(configuration["weeksOfWork"]);
            _logger.LogInformation("get all pizzas");
            return _pizzaService.GetAll();
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<Pizza> GetPizzaById(int id)
        {
            var pizza = _pizzaService.GetById(id);
            if(pizza == null)
            {
                _logger.LogError("error in fetching get pizza by id");
                return NotFound();
            }
            return pizza;
          
        }

        [HttpPost("create")]
        public IActionResult Create(Pizza pizza)
        {
            if(pizza == null)
            {
                return BadRequest();
            }

             _pizzaService.AddPizza(pizza);
            return CreatedAtAction(nameof(Create),new Pizza { Id=pizza.Id},pizza);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if(id!=pizza.Id)
            {
                return BadRequest("Id is not matched");
            }
 
            pizza =_pizzaService.update(id, pizza); 
            if(pizza == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = _pizzaService.GetById(id);
            if(pizza == null)
            {
                return NotFound();
            }
           
            _pizzaService.DeleteById(id);
            return NoContent();
      

        }
    }
}
