using System.Security.Claims;
using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    //create user
    Task<User> Create(UserCreationDto dto);
    
    //log in 
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    
    Task<ICollection<User>> GetAsync(string? userName, int? userId, int? companyId, string? role); // to get list of users
    Task DeleteAsync(int userId);  //delete user
}