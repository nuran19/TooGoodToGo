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
}