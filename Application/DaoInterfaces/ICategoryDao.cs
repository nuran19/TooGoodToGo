using Domain.Models;

namespace Application.DaoInterfaces;

public interface ICategoryDao
{
    public Task<IEnumerable<Category>> GetAsync();
}