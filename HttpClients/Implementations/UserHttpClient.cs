using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;   //
    
    //log in 
    // this private variable for simple caching
    public static string? Jwt { get; private set; } = "";
    
    //log in 
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    public UserHttpClient(HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }

    public async Task<User> Create(UserCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto); //We use the client to make a POST request to "/users", sending the dto
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;  //the result is a User as JSON, and it is deserialized and returned.
        return user;  //Every request returns a response, whether we actually expect an object back or not(success/error message)
    }
    

 
//list of users 
    
    public async Task<ICollection<User>> GetAsync(string? userName, int? userId, int? companyId, string? roleContains)
    {
        string query = ConstructQuery(userName, userId, companyId, roleContains);
        HttpResponseMessage response = await client.GetAsync("/users" + query);

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return users;
    }

    private static string ConstructQuery(string? userName, int? userId, int? companyId, string? roleContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"?username={userName}";
        }

        if (userId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"userid={userId}";
        }

        if (companyId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"companyId={companyId}";
        }

        if (!string.IsNullOrEmpty(roleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"roleContains={roleContains}";
        }

        return query;
    }

    
   //log in 
     public async Task LoginAsync(string username, string password)
    {
        UserLoginDto userLoginDto = new()
        {
            Username = username,
            Password = password
        };

        string userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("http://localhost:5258/auth/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
   
        
        string token = responseContent;
        Jwt = token;

        ClaimsPrincipal principal = CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);
        
    }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
        
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    // public async Task RegisterAsync(User user)
    // {
    //     string userAsJson = JsonSerializer.Serialize(user);
    //     StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
    //     HttpResponseMessage response = await client.PostAsync("https://localhost:5258/auth/register", content);
    //     string responseContent = await response.Content.ReadAsStringAsync();
    //
    //     if (!response.IsSuccessStatusCode)
    //     {
    //         throw new Exception(responseContent);
    //     }
    // }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }
    
   
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
    
    //Delete user by id 
    public async Task DeleteAsync(int userId)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Users/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

}