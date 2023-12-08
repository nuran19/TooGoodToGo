using Domain.Models;

namespace Application.DaoInterfaces;

public interface IDayContentProductDao
{
    
    Task<DayContentProduct> CreateAsync(DayContentProduct dayContentProduct);
    Task<ICollection<DayContentProduct>> CreateAsync(int dayContentId, List<DayContentProduct> dayContentProducts);
    
    // This requires change in IDayContentProductEfcDao ----
    Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId);

    Task<ICollection<DayContentProductDetail>> GetDayContentDetailAsync(int dayContentId);
    

    Task UpdateAsync(DayContentProduct dayContentProduct);
    
    Task DeleteAsync(DayContentProduct dayContentProduct);
    Task<DayContentProduct?> GetByIdAsync(int dayContentId, int productId);
}