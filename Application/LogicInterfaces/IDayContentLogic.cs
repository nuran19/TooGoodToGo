using Domain.DTOs;
using Domain.Models;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IDayContentLogic
{
    Task<DayContent> CreateAsync(DayContentCreationDto dto);
    
    //view Daycontent - search/list
    Task<IEnumerable<DayContent>> GetAsync(SearchDayContentParametersDto searchParameters);
  
    // view single DayContent ??????
    Task<DayContentBasicDto> GetByIdAsync(int id);
    
    // Extra method implemented in DayCintentLogic and DayContentController
    Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId);

    Task<ICollection<DayContentProductDetail>> GetDayContentDetailAsync(int dayContentId);
    
    Task<List<DayContent>> GetMonthEntries(int compId,int month, int year);
    Task<List<DayContent>> GetEntriesForDateRange(int compId,DateOnly startDate, DateOnly endDate);
    
    // Task<DayContent> UpdateAsync(int dayContentId, DayContentUpdateDto dto);
    
    Task<DayContent> UpdateAsync(DayContentUpdateDto dto);
    
    //Task DeleteAsync(int dayContentId);
    Task DeleteAsync(int dayContentId,bool deleteProducts = false, List<int> productIds = null);
    Task UpdateRelatedProducts(int dayContentId, List<DayContentProductUpdateDto> productDtos);
}