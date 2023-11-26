using System.Text.Json;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class SubCategoryHttpClient: ISubCategoryService
{
    
    private readonly HttpClient client;

    public SubCategoryHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<SubCategory>> GetSubCategories(int catId)
    {
        string uri = "/subCategory";
        if ( catId != 0 )
        {
            uri += $"?categoryId={catId}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<SubCategory> subCategories = JsonSerializer.Deserialize<IEnumerable<SubCategory>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return subCategories;
    }
}