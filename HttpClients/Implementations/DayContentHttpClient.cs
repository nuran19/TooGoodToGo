using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Shared.Models;

namespace HttpClients.Implementations;

public class DayContentHttpClient : IDayContentService
{
    private readonly HttpClient client;

    public DayContentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    //create dayContent -make a POST request with the JSON.
    public async Task<int> CreateAsync(DayContentCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/dayContents", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        else
        {
            DayContent dayContent = await response.Content.ReadFromJsonAsync<DayContent>();
            return dayContent.Id;
        }


    }

    //view all dayContents
    public async Task<ICollection<DayContent>> GetAsync(string? userName, int? userId, DateOnly date, string? reason)
    {
        string query = ConstructQuery(userName, userId, date, reason);
        HttpResponseMessage response = await client.GetAsync("/products" + query);

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<DayContent> dayContents = JsonSerializer.Deserialize<ICollection<DayContent>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return dayContents;
    }
    
    public async Task<ICollection<DayContentProductDetail>> GetDayContentDetailAsync(int dayContentId)
    {
        try
        {
            string endpoint = $"/dayContents/{dayContentId}/products";

            HttpResponseMessage response = await client.GetAsync(endpoint);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            ICollection<DayContentProductDetail> dayContentProducts = JsonSerializer.Deserialize<ICollection<DayContentProductDetail>>(
                content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;

            return dayContentProducts;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }
    public async Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId)
    {
        try
        {
            string endpoint = $"/dayContents/{dayContentId}/products";

            HttpResponseMessage response = await client.GetAsync(endpoint);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            ICollection<DayContentProduct> dayContentProducts = JsonSerializer.Deserialize<ICollection<DayContentProduct>>(
                content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;

            return dayContentProducts;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }

    //search products with filtering  
    //It will check each filter argument, check if they are not null, in which case they should be ignore.
    //And otherwise include the needed filter argument in the query parameter string.
    private static string ConstructQuery(string? userName, int? userId, DateOnly date, string? reason)
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

        if (date != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"companyId={date}";
        }

        if (!string.IsNullOrEmpty(reason))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"subCategoryContains={reason}";
        }

        return query;
    }

    public async Task<DayContentBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/dayContents/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        DayContentBasicDto dayContent = JsonSerializer.Deserialize<DayContentBasicDto>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!; // null-suppressor "!"
        return dayContent;
    }
    
    public async Task<List<DayContent>> GetMonthEntries(int month, int year)
    {
        string query = $"?month={month}&year={year}";
        HttpResponseMessage response = await client.GetAsync($"/DayContents/month-entries{query}");
        
        string content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        List<DayContent> entries = JsonSerializer.Deserialize<List<DayContent>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return entries;
    }

    public async Task<List<DayContent>> GetEntriesForDateRange(DateOnly startDate, DateOnly  endDate)
    {
        string query = $"?startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}";
        HttpResponseMessage response = await client.GetAsync($"/DayContents/date-range-entries{query}");

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        List<DayContent> entries = JsonSerializer.Deserialize<List<DayContent>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return entries;
    }
    // if deleteDayContent=true, means delete day content and products else delete only products in the list
    public async Task DeleteAsync(int id, bool deleteDayContent, List<int> productIds)
    { 
        //80?deleteDayContent=false&productIdsToDelete=2&productIdsToDelete=1 
        string query=$"{id}?deleteDayContent="+deleteDayContent;
        if (productIds.Any())
        {
            foreach (var pID in productIds)
            {
                query += "&productIdsToDelete=" + pID;
            }
           
        }
        HttpResponseMessage response = await client.DeleteAsync($"DayContents/"+query);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task UpdateAsync(DayContentUpdateDto dto)
    {
      
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PatchAsync($"/DayContents", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

}