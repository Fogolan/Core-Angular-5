namespace PartyPlanner.Api.Services.Auth.Models
{
  public class Credentials
  {
    public string Username { get; set; }

    public string Token { get; set; }

    public int ExpiresIn { get; set; }

    public string Id { get; set; }
  }
}