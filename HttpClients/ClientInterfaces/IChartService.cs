using Domain.Models;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IChartService
{
    Task<ICollection<DayContentProduct>> GetAsync(int companyId, string? productTypeContains);
        // string? categoryContains,
        // string? subCategoryContains,
        // string? reasonContains,
        // int? dayQuantityContains,
        // DateOnly? date);
        
        Task<ICollection<DayContentProduct>> GetAsyncMore(int companyId, 
            string? productTypeContains,
        string? categoryContains,
        string? subCategoryContains,
        string? reasonContains,
        int? dayQuantityContains,
        DateOnly? date);
}