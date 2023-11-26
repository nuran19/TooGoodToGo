using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ICategoryService
{
   Task<IEnumerable<Category>> GetCategories();
}