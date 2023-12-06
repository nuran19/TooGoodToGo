using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductLogic productLogic;

    public ProductsController(IProductLogic productLogic)
    {
        this.productLogic = productLogic;
    }
    
    
    //endpoint
    //create product
    [HttpPost]
    public async Task<ActionResult<Product>> CreateAsync([FromBody]ProductCreationDto dto)
    {
        try
        {
            Product created = await productLogic.CreateAsync(dto);
          //  return Created($"/SinglePost/{created.Id}", created);  //posts
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //endpoint 
    //get/retrival of posts
 /*   [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId, [FromQuery] string? titleContains, [FromQuery] string? bodyContains)
    {
        try
        {
            SearchProductParametersDto parameters = new ( userName, userId, titleContains, bodyContains);
            var posts = await productLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    } */
    
 
 //endpoint
 //return list of titles after search of specific criteria
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId, [FromQuery]int? companyId, [FromQuery] string? subCategoryContains,   [FromQuery] string? typeContains, [FromQuery] string? brandContains) {
        try
        {
            SearchProductParametersDto parameters = new ( userName, userId, companyId, subCategoryContains, typeContains, brandContains);
            var products = await productLogic.GetAsync(parameters);
            return Ok(products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //endpoint 
    //GET -return a single product
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            ProductBasicDto result = await productLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //delete prod
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await productLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //drop down products
    [HttpGet ("GetProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int subCategoryId)
    {
        try
        {
            //  SearchSubCategoryParametersDto parameters = new(categoryId);
            IEnumerable<Product> products = await productLogic.GetProducts(subCategoryId);
            return Ok (products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}