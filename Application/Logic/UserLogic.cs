using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;
    

    public UserLogic(IUserDao userDao)  //User DAO is received through constructor dependency injection
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password,
            CompanyId = dto.CompanyId,
            Role = dto.Role
        };
        //A new User object is created, and handed over to the DAO for storage.
        //  We return the newly created User object, now with an ID too. This ID is generated in the Data layer.
        User created = await userDao.CreateAsync(toCreate);
        
        return created;
    }

    //just checks the rules of the username
    //The method is static because it is a utility method. It just takes an argument, does something with that and either returns void or some object. We don't use any field variables.
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;
        string password = userToCreate.Password;
        string role = userToCreate.Role;
        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        
        if(password.Length < 8)
            throw new Exception("Password must be at least 8 characters!");
        if(!role.Equals("Employee") && !role.Equals("Manager"))
            throw new Exception("Role must be Employee or Manager!");
         
    } 

    
    
    //log in
    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;  
    }
}