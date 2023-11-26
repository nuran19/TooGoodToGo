using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ISubCategoryService
{
    Task<IEnumerable<SubCategory>> GetSubCategories(int catId);
}