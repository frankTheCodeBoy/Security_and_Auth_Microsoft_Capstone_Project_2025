using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

[TestFixture]
public class TestAuthAndRBAC
{
    private readonly HttpClient _client = new HttpClient { BaseAddress = new System.Uri("http://localhost:5000/") };

    [Test]
    public async Task TestInvalidLogin()
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", "fakeuser"),
            new KeyValuePair<string, string>("password", "wrongpass")
        });
        var response = await _client.PostAsync("Auth/login", content);
        Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, response.StatusCode, "Invalid login should be unauthorized.");
    }

    [Test]
    public async Task TestValidLogin()
    {
        // Assumes a valid user "adminuser" with password "AdminPass123"
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", "adminuser"),
            new KeyValuePair<string, string>("password", "AdminPass123")
        });
        var response = await _client.PostAsync("Auth/login", content);
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Valid login should succeed.");
    }

    [Test]
    public async Task TestAdminAccessControl()
    {
        // Simulate login as regular user
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", "regularuser"),
            new KeyValuePair<string, string>("password", "Regular123")
        });
        await _client.PostAsync("Auth/login", content);

        // Try to access admin dashboard
        var response = await _client.GetAsync("Admin/dashboard");
        Assert.AreEqual(System.Net.HttpStatusCode.Forbidden, response.StatusCode, "Non-admin should not access admin dashboard.");
    }

    [Test]
    public async Task TestAdminAccessAllowed()
    {
        // Simulate login as admin
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", "adminuser"),
            new KeyValuePair<string, string>("password", "AdminPass123")
        });
        await _client.PostAsync("Auth/login", content);

        // Try to access admin dashboard
        var response = await _client.GetAsync("Admin/dashboard");
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Admin should access admin dashboard.");
    }
}