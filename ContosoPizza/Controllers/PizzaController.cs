using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        public PizzaController() 
        {
            
        }

        //GET all action
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll()
        {
            return PizzaService.GetAll();
        }

        //GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if(pizza == null)
            {
                return NotFound();
            }
            return pizza;
        }

        //POST action
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            PizzaService.Add(pizza);
            // CreatedAtAction Create 201  response with location head
            //nameof(Get) get the name of the GET method retrieve the newly created resource.
            //new {id= pizza.Id} create anonymous object with id property set to the Id of pizza object
            //pizza body of the response contains pizza object
            return CreatedAtAction(nameof(Get), new {id= pizza.Id},pizza);
        }


        //PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id,Pizza pizza)
        {
            if(id !=pizza.Id)
            {
                return BadRequest();
            }
            var existingPizza = PizzaService.Get(id);
            if (existingPizza == null)
            {
                return NotFound();
            }
            PizzaService.Update(pizza);
            //return HTTP status code 204 NO Content
            return NoContent();
        }
        //DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);
            if(pizza is null)
            {
                return NotFound();
            }
            PizzaService.Delete(id);
            return NoContent();
        }


    }
}
