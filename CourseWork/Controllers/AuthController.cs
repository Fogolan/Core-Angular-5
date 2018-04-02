using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.Auth;

namespace PartyPlanner.Web.Api.Controllers
{
  [Produces("application/json")]
  [Microsoft.AspNetCore.Mvc.Route("api/login")]
  [IgnoreAntiforgeryToken]
  public class AuthController : Controller
  {
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
      this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] Login.Query query)
    {
      return Ok(await mediator.Send(query));
    }
  }
}