using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

[TestFixture]
public class TestInputValidation
{
    private readonly HttpClient _client = new HttpClient { BaseAddress = new System.Uri("http://localhost:5000/") };

    [Test]
    public async Task TestForSQLInjection()
    {
        var maliciousInput = "Robert'); DROP TABLE Users;--";
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", maliciousInput),
            new KeyValuePair<string, string>("email", "attacker@example.com")
        });

        var response = await _client.PostAsync("User/submit", content);
        string responseBody = await response.Content.ReadAsStringAsync();

        Assert.IsTrue(
            response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
            !responseBody.Contains("DROP TABLE"), 
            "SQL Injection was not prevented!");
    }

    [Test]
    public async Task TestForXSS()
    {
        var xssInput = "<script>alert('XSS');</script>";
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", xssInput),
            new KeyValuePair<string, string>("email", "xss@example.com")
        });

        var response = await _client.PostAsync("User/submit", content);
        string responseBody = await response.Content.ReadAsStringAsync();

        Assert.IsTrue(
            response.StatusCode == System.Net.HttpStatusCode.BadRequest,
            "XSS input was accepted!");
    }

    [Test]
    public async Task TestForValidInput()
    {
        var validInput = "ValidUser_123";
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", validInput),
            new KeyValuePair<string, string>("email", "validuser@example.com")
        });

        var response = await _client.PostAsync("User/submit", content);
        string responseBody = await response.Content.ReadAsStringAsync();

        Assert.IsTrue(
            response.StatusCode == System.Net.HttpStatusCode.OK &&
            responseBody.Contains("User registered securely"),
            "Valid input was rejected!");
    }
}