using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user); //the responsibility of converting from UserCreationDto to User lies in the application layer
    Task<User?> GetByUsernameAsync(string userName);
    
    //add product
    Task<User?> GetByIdAsync(int id);
}