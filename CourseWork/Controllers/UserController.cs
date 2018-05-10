using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PartyPlanner.Api.Services.Auth;
using PartyPlanner.Api.Services.User;

namespace PartyPlanner.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [IgnoreAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly IMediator mediator;
        private readonly RoleManager<IdentityRole> roleManager;

    public UserController(IMediator mediator, RoleManager<IdentityRole> roleManager)
    {
      this.mediator = mediator;
      this.roleManager = roleManager;
    }

      [HttpPost]
      public async Task<IActionResult> CreateUser([FromBody] CreateUser.Command command)
      {
          command.UrlHelper = new UrlHelper(Url.ActionContext);
          return Ok(await mediator.Send(command));
      }

      [HttpPost("role")]
      public async Task<IActionResult> CreateRole([FromBody] CreateUser.Command command)
      {
        IdentityResult result = await roleManager.CreateAsync(new IdentityRole("user"));

        return Ok();
      }

        [HttpGet("confirm")]
        public async Task ConfirmEmail(ConfirmEmail.Query query)
        {
            var result = await mediator.Send(query);
            string url = $"http://localhost:4200/confirm?confirmSuccess={result}";
            Response.Redirect(url);
        }
    }
}
