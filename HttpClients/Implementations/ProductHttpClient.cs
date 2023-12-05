using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class ProductHttpClient : IProductService
{
    private readonly HttpClient client;

    public ProductHttpClient(HttpClient client)
    {
        this.client = client;
    }
//create product -make a POST request with the JSON. 
    public async Task<int> CreateAsync(ProductCreationDto dto)
    {
        
        HttpResponseMessage response = await client.PostAsJsonAsync("/products",dto); //posts
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        Product product = await response.Content.ReadFromJsonAsync<Product>();
        return product.Id;

    } 
    
    //view all products
    public async Task<ICollection<Product>> GetAsync(string? userName, int? userId, int?companyId, string? subcategory, string? typeContains,string? brandContains)
    {
      //  HttpResponseMessage response = await client.GetAsync("/products");  //making a GET request, checking the status code, and deserializing the response
      string query = ConstructQuery(userName, userId, companyId, subcategory, typeContains, brandContains);
      HttpResponseMessage response = await client.GetAsync("/products"+query);
      
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Product> products = JsonSerializer.Deserialize<ICollection<Product>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        //FOR COMPANY ID VIEW PROD
        // ICollection<Product> productsFiltered = (ICollection<Product>)products.Where(p => p.CompanyId == companyId);
        // return productsFiltered;
        return products;
    }
   
    //search products with filtering  
    //It will check each filter argument, check if they are not null, in which case they should be ignore.
    //And otherwise include the needed filter argument in the query parameter string.
    private static string ConstructQuery(string? userName, int? userId, int?companyId,  string? subCategoryContains, string? typeContains, string? brandContains, int? dayContentId = null)
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

        if (!string.IsNullOrEmpty(subCategoryContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"subCategoryContains={subCategoryContains}";
        }
        if (!string.IsNullOrEmpty(typeContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"typeContains={typeContains}";
        }
        if (!string.IsNullOrEmpty(brandContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"brandContains={brandContains}";
        }
        
        if (dayContentId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"dayContentId={dayContentId}";
        }

        return query;
    }
    
    //get product by id 
    
    public async Task<ProductBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/products/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ProductBasicDto product = JsonSerializer.Deserialize<ProductBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return product;
    }

    //delete product
    
    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Products/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
    //Get products method

    public async Task<IEnumerable<Product>> GetProducts(int subCatId)
    {
        string uri = "/products";
        if (subCatId != 0)
        {
            uri += $"?subCategoryId={subCatId}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Product> products = JsonSerializer.Deserialize<IEnumerable<Product>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return products;
    }

}