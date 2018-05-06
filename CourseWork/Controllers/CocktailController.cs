using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.Cocktail;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Web.Api.Controllers
{
    [Route("api/Cocktail")]
    public class CocktailController : Controller
    {
        private readonly IMediator _mediator;

        public CocktailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Cocktail
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetCocktails.Query()));
        }

        // GET: api/Cocktail/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCocktail([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetCocktail.Query
            {
                Id = id
            }));
        }

        // PUT: api/Cocktail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCocktail([FromRoute] int id, [FromBody] Cocktail cocktail)
        {
            return Ok(await _mediator.Send(new UpdateCocktail.Command
            {
                Cocktail = cocktail,
                Id = id
            }));
        }

        // POST: api/Cocktail
        [HttpPost]
        public async Task<IActionResult> CreateCocktail([FromBody] Cocktail cocktail)
        {
            return Ok(await _mediator.Send(new CreateCocktail.Command
            {
                Cocktail = cocktail
            }));
        }

        // DELETE: api/Cocktail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCocktail([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteCocktail.Command
            {
                Id = id
            }));
        }

    }
}