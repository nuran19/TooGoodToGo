using Application.LogicInterfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryLogic catLogic;
    
    public CategoryController(ICategoryLogic catLogic)
    {
        this.catLogic =  catLogic;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAsync()
    {
        try
        {
            IEnumerable<Category> categories = await catLogic.GetAsync();
            return Ok(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}