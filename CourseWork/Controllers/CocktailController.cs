using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.Cocktail;

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
        public async Task<IActionResult> UpdateCocktail([FromBody] UpdateCocktail.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        // POST: api/Cocktail
        [HttpPost]
        public async Task<IActionResult> CreateCocktail([FromBody] CreateCocktail.Command command)
        {
            return Ok(await _mediator.Send(command));
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