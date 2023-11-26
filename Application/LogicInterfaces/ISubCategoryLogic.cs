using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ISubCategoryLogic
{
    Task<IEnumerable<SubCategory>> GetAsync(int categoryId);
}