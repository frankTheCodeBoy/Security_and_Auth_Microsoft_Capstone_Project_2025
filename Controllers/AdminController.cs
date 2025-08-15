using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet("dashboard")]
    [AuthorizeRole("admin")]
    public IActionResult Dashboard()
    {
        // Admin-only feature
        return Ok("Welcome to the Admin Dashboard!");
    }
}