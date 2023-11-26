using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ISubCategoryDao
{
    public Task<IEnumerable<SubCategory>> GetAsync(int categoryId);
}