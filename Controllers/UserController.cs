using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly string _connectionString = "YourConnectionStringHere";

    [HttpPost("submit")]
    public IActionResult Submit([FromForm] string username, [FromForm] string email)
    {
        var sanitizedUsername = InputSanitizer.SanitizeUsername(username);
        var sanitizedEmail = InputSanitizer.SanitizeEmail(email);

        if (string.IsNullOrEmpty(sanitizedUsername) || string.IsNullOrEmpty(sanitizedEmail))
            return BadRequest("Invalid username or email.");

        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("INSERT INTO Users (Username, Email) VALUES (@Username, @Email)", conn))
        {
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = sanitizedUsername;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = sanitizedEmail;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        return Ok("User registered securely.");
    }

    [HttpGet("find")]
    public IActionResult Find([FromQuery] string username)
    {
        var sanitizedUsername = InputSanitizer.SanitizeUsername(username);

        if (string.IsNullOrEmpty(sanitizedUsername))
            return BadRequest("Invalid username.");

        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("SELECT UserID, Username, Email FROM Users WHERE Username = @Username", conn))
        {
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = sanitizedUsername;
            conn.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Ok(new
                    {
                        UserID = reader["UserID"],
                        Username = InputSanitizer.HtmlEncode(reader["Username"].ToString()),
                        Email = InputSanitizer.HtmlEncode(reader["Email"].ToString())
                    });
                }
            }
        }
        return NotFound();
    }
}