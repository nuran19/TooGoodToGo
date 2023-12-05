using Domain.Models;

namespace Application.DaoInterfaces;

public interface IDayContentProductDao
{
    
    Task<DayContentProduct> CreateAsync(DayContentProduct dayContentProduct);
    Task<ICollection<DayContentProduct>> CreateAsync(int dayContentId, List<DayContentProduct> dayContentProducts);
    
    // This requires change in IDayContentProductEfcDao ----
    Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId);

    Task<ICollection<DayContentProductDetail>> GetDayContentDetailAsync(int dayContentId);

    // Task<IEnumerable<DayContentProduct>> GetAsync(SearchDayContentProductParametersDto searchParameters);


    // Task<DayContentProduct?> GetByIdAsync(int dayContentid);
}