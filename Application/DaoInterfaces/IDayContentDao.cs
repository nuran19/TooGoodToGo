using Domain.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IDayContentDao
{
    Task<DayContent> CreateAsync(DayContent dayContent);
    
    //view Daycontent - search/list
    Task<IEnumerable<DayContent>> GetAsync(SearchDayContentParametersDto searchParameters);
  
    // view single DayContent ??????
    Task<DayContent?> GetByIdAsync(int dayContentid);
    
    //---Required for update
    Task UpdateAsync(DayContent dayContent);
    //Task UpdateAsync(DayContentUpdateDto dto);
    
    Task DeleteAsync(int dayContent);
    
    Task<List<DayContent>> GetMonthEntries(int month, int year);
    Task<List<DayContent>> GetEntriesForDateRange(DateOnly startDate, DateOnly endDate);
}