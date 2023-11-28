using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate); // return type is Task<User> because we may want to do some work asynchronously.
    // //log in 
     Task<User> ValidateUser(string name, string password); 
     
     public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters); //search user params
     

     Task DeleteAsync(int userId);
}