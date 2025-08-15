using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly string _connectionString = "YourConnectionStringHere";

    [HttpPost("login")]
    public IActionResult Login([FromForm] string username, [FromForm] string password)
    {
        User user = null;
        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("SELECT UserID, Username, PasswordHash, Role FROM Users WHERE Username=@Username", conn))
        {
            cmd.Parameters.AddWithValue("@Username", username);
            conn.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = new User {
                        UserID = (int)reader["UserID"],
                        Username = reader["Username"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }
        }
        if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
            return Unauthorized("Invalid username or password.");

        // In a real system, generate a JWT or secure session cookie here
        HttpContext.Session.SetInt32("UserID", user.UserID);
        HttpContext.Session.SetString("Role", user.Role);
        return Ok("Login successful.");
    }
}