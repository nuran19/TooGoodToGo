using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SubCategoryController : ControllerBase
{
    private readonly ISubCategoryLogic subCatLogic;

    public SubCategoryController(ISubCategoryLogic subCatLogic)
    {
        this.subCatLogic = subCatLogic;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubCategory>>> GetAsync([FromQuery] int categoryId)
    {
        try
        {
          //  SearchSubCategoryParametersDto parameters = new(categoryId);
            IEnumerable<SubCategory> subCategories = await subCatLogic.GetAsync(categoryId);
            return Ok (subCategories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
            
            
           