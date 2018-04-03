using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PartyPlanner.Web.Api.Controllers
{
  [Authorize]
  [Produces("application/json")]
  [Route("api/home")]
  public class HomeController : Controller
  {
    [HttpPost]
    public IActionResult Home()
    {
      return Ok();
    }
  }
}