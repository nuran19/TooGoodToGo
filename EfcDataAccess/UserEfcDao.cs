using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class UserEfcDao : IUserDao
{
    private readonly PostContext context;

    public UserEfcDao(PostContext context)
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
}
