using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class UserEfcDao : IUserDao
{
    private readonly TGTGContext context;

    public UserEfcDao(TGTGContext context)
    {
        this.context = context;
    }
    //add user  //database sets the Id of the User
    public async Task<User> CreateAsync(User user) 
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    } 

    //before adding a new User, the logic layer will ask whether the selected user name is taken
   public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(userName.ToLower()));  // make both user names lower case and compare.
        return existing;
    }

  
   
   //view users
   public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)  
   {
       IQueryable<User> query = context.Users.AsQueryable(); 

       if (!string.IsNullOrEmpty(searchParameters.UsernameContains))
       {
           // we know username is unique, so just fetch the first
           query = query.Where(user =>
               user.UserName.ToLower().Equals(searchParameters.UsernameContains.ToLower()));
       }

       if (searchParameters.UserIdContains != null)
       {
           query = query.Where(t => t.Id == searchParameters.UserIdContains);
       }
       if (searchParameters.CompanyIdContains != null)
       {
           query = query.Where(t => t.CompanyId == searchParameters.CompanyIdContains);
       }
    
       if (!string.IsNullOrEmpty(searchParameters.RoleContains))
       {
           query = query.Where(t =>
               t.Role.ToLower().Contains(searchParameters.RoleContains.ToLower()));
       }
    
       List<User> result = await query.ToListAsync(); //executing against the database 
       return result;
   }

   
   
   //add post 
   public async Task<User?> GetByIdAsync(int id)
   {
       User? user = await context.Users.FindAsync(id);
       return user;
   }
   
   //delete user 
   public async Task DeleteAsync(int userId)
   {
       User? existing = await GetByIdAsync(userId);
       if (existing == null)
       {
           throw new Exception($"User with id {userId} not found");
       }

       context.Users.Remove(existing);
       await context.SaveChangesAsync();
   }

}
