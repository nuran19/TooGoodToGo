using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
//responsible for everything User object related


[ApiController] // marks this class as a Web API controller
[Route("[controller]")] //specifies the sub-URI to access this controller class.-->"route template", the URI will be localhost:port/users
//We can define our own path with fx [Route("api/users")], and then the URI would be localhost:port/api/users

public class UsersController : ControllerBase // access to various utility methods.
{
    private readonly IUserLogic userLogic; // a field variable, injected through the constructor, so we can get access to the application layer, i.e. the logic.
    public UsersController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }


    //endpoint
    //Web API endpoint which the client can call to create a new User object

    [HttpPost] //POST requests to users should hit this endpoint.
    public async Task<ActionResult<User>> CreateAsync( UserCreationDto dto) // async, to support asynchronous work. The return type is as a consequence a Task. This Task contains an ActionResult with a User inside. 
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return
                Created($"/users/{user.Id}", user); //The resulting User is then returned, with the method Created(), which will create an ActionResult with status code 201, the new path to this specific User (the endpoint of which we haven't made yet, but probably will), and finally the user object is also included
        } //?????????????????????????????????????????????????????????????????????????????????
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}
// In our case the server only sets the ID.
// But in other cases, all kinds of data can be set or modified when creating an object,
// so generally it is polite to return the result, so the client/user can verify the result.

//

