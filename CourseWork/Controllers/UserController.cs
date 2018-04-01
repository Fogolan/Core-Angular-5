using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.User;

namespace PartyPlanner.Web.Api.Controllers
{
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("api/User")]
    [IgnoreAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Command command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
