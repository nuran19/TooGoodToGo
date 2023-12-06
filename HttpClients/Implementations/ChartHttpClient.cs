using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Shared.Models;

namespace HttpClients.Implementations;

public class ChartHttpClient:IChartService
{
    
    private readonly HttpClient client;

    public ChartHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<ICollection<DayContentProduct>> GetAsync(int companyId, string? productTypeContains)
    {
        string query = ConstructQuery (companyId,  productTypeContains);
        HttpResponseMessage response = await client.GetAsync("/chart" + query);  
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<DayContentProduct> chartData = JsonSerializer.Deserialize<ICollection<DayContentProduct>>(content, new JsonSerializerOptions
        {
          ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNameCaseInsensitive = true
            
        })!;
        // if (chartData != null)
        // {
        //     foreach (DayContentProduct dcp in chartData)
        //     {
        //         HttpResponseMessage response2 = await client.GetAsync("/products" + "?" + $"Id={dcp.ProductId}");  //?????????????????????
        //         string content2 = await response2.Content.ReadAsStringAsync();
        //         ICollection<Product> products = JsonSerializer.Deserialize<ICollection<Product>>(content2, new JsonSerializerOptions
        //         {
        //             PropertyNameCaseInsensitive = true
        //         })!;
        //         if (products.Count > 0)
        //         {
        //             dcp.Product = products.First();
        //         }
        //         
        //         HttpResponseMessage response3 = await client.GetAsync("/dayContent" + "?" + $"Id={dcp.DayContentId}");  //?????????????????????
        //         string content3 = await response3.Content.ReadAsStringAsync();
        //         ICollection<DayContent> dayContents = JsonSerializer.Deserialize<ICollection<DayContent>>(content3, new JsonSerializerOptions
        //         {
        //             PropertyNameCaseInsensitive = true
        //         })!;
        //         if (dayContents.Count > 0)
        //         {
        //             dcp.DayContent = dayContents.First();
        //         }
        //     }
        // }
        return chartData;
    }

    private static string ConstructQuery(int companyId,string? productTypeContains)
    {
        string query = "";
        if (companyId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"companyId={companyId}";
        }
        
        if (!string.IsNullOrEmpty(productTypeContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"productTypeContains={productTypeContains}";
        }
        return query;
    }
    
    //
    public async Task<ICollection<DayContentProduct>> GetAsyncMore(int companyId, string? categoryContains,
        string? subCategoryContains, string? productTypeContains, string? reasonContains, int? dayQuantityContains,
        DateOnly? date)
    {
        string query = ConstructQueryMore (companyId, categoryContains, subCategoryContains, productTypeContains, reasonContains, dayQuantityContains, date);
        HttpResponseMessage response = await client.GetAsync("/chart" + query);  //?????????????????????
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    
        ICollection<DayContentProduct> chartData = JsonSerializer.Deserialize<ICollection<DayContentProduct>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        })!;
        return chartData;
    }
    
    private static string ConstructQueryMore(int companyId, string? categoryContains, string? subCategoryContains, string? productTypeContains, string? reasonContains ,int? dayQuantityContains, DateOnly? date)
    {
        string query = "";
        if (companyId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"companyId={companyId}";
        }
        
        if (!string.IsNullOrEmpty(categoryContains))
        {
            query += $"?categoryContains={categoryContains}";
        }
        if (!string.IsNullOrEmpty(subCategoryContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"subCategoryContains={subCategoryContains}";
        }
        
        if (!string.IsNullOrEmpty(productTypeContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"productTypeContains={productTypeContains}";
        }
        if (!string.IsNullOrEmpty(reasonContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"reasonContains={reasonContains}";
        }
        if (dayQuantityContains != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"dayQuantityContains={dayQuantityContains}";
        }
        if (date != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"date={date}";
        }
        return query;
    }

}