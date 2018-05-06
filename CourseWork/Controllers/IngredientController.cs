using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.Ingredient;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Web.Api.Controllers
{
    [Route("api/Ingredient")]
    public class IngredientController : Controller
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/Ingredient
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Ingredient
        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] Ingredient ingredient)
        {
            return Ok(await _mediator.Send(new CreateIngredient.Command
            {
                Ingredient = ingredient
            }));
        }
   
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteIngredient.Command
            {
                Id = id
            }));
        }
    }
}
