using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user); //the responsibility of converting from UserCreationDto to User lies in the application layer
    Task<User?> GetByUsernameAsync(string userName);
    
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);

    //add product
    Task<User?> GetByIdAsync(int id);
    
    Task DeleteAsync(int userId);

}