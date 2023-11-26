using System.Text.Json;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CategoryHttpClient: ICategoryService
{
    private readonly HttpClient client; 
    
    public CategoryHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<IEnumerable<Category>> GetCategories()
    {
        string uri = "/category";
        
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Category> subcategories = JsonSerializer.Deserialize<IEnumerable<Category>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return subcategories;
    }
    
}