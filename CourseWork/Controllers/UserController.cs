using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.User;
using PartyPlanner.Api.Services.User.Models;

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
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            return Ok(await mediator.Send(new CreateUser.Command
            {
                UserDto = userDto
            }));
        }
    }
}
