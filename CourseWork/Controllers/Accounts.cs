using Microsoft.AspNetCore.Mvc;

namespace PartyPlanner.Web.Api.Controllers
{
  public class Accounts : Controller
  {
    // GET
    public IActionResult Index()
    {
      return
      View();
    }
  }
}