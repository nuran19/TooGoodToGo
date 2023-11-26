using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICategoryLogic
{ 
    Task<IEnumerable<Category>> GetAsync();
}