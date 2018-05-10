using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.Ingredient;
using PartyPlanner.Api.Services.Ingredient.Models;

namespace PartyPlanner.Web.Api.Controllers
{
    [Authorize]
    [Route("api/Ingredient")]
    public class IngredientController : Controller
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetIngredient.Query
            {
                Id = id
            }));
        }
        
        // POST: api/Ingredient
        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDto ingredient)
        {
            return Ok(await _mediator.Send(new CreateIngredient.Command
            {
                Ingredient = ingredient,
                UserClaims = User
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
