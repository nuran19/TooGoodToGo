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

    //add post 
   public async Task<User?> GetByIdAsync(int id)
   {
       User? user = await context.Users.FindAsync(id);
       return user;
    }
   public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
   {
       IQueryable<User> usersQuery = context.Users.AsQueryable();
       if (searchParameters.UsernameContains != null)
       {
           usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(searchParameters.UsernameContains.ToLower()));
       }

       IEnumerable<User> result = await usersQuery.ToListAsync();
       return result;
   }
   
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
