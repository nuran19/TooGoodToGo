using Domain.DTOs;
using Domain.Models;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IDayContentService
{
    //to create dayContent
    Task<int> CreateAsync( DayContentCreationDto dto); 
    
    //view all daycontents
    //   Task<ICollection<string>> GetAsync(string? titleContains)
    Task<ICollection<DayContent>> GetAsync( 
        string? userName, 
        int? userId, 
        DateOnly date,
        string? reason
    );

    /// <summary>
    /// Get all products for a day content item
    /// </summary>
    Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId);
 
    Task<ICollection<DayContentProductDetail>> GetDayContentDetailAsync(int dayContentId);
    //get dayContent by id 
    Task<DayContentBasicDto> GetByIdAsync(int id);
    
    Task<List<DayContent>> GetMonthEntries(int compId, int month, int year);
    Task<List<DayContent>> GetEntriesForDateRange(int compId, DateOnly startDate, DateOnly endDate);
    
    Task DeleteAsync(int id, bool deleteDayContent, List<int> productIds);
    Task UpdateAsync(DayContentUpdateDto dto);
}
