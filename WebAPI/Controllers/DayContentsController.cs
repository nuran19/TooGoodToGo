using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class DayContentsController : ControllerBase
{
    private readonly IDayContentLogic dayContentLogic;

    public DayContentsController(IDayContentLogic dayContentLogic)
    {
        this.dayContentLogic = dayContentLogic;
    }

    [HttpPost]
    public async Task<ActionResult<DayContent>> CreateAsync([FromBody] DayContentCreationDto dto)
    {
        try
        {
           DayContent created = await dayContentLogic.CreateAsync(dto);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DayContent>>> GetAsync([FromQuery] string? userName,
        [FromQuery] int? userId, [FromQuery] DateOnly date, [FromQuery] int productId)
    {
        try
        {
            SearchDayContentParametersDto parameters =
                new SearchDayContentParametersDto(userName, userId, date, productId);
            var dayContents = await dayContentLogic.GetAsync(parameters);
            return Ok(dayContents);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DayContentBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            DayContentBasicDto result = await dayContentLogic.GetByIdAsync(id);
            return Ok(result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
   // Returns details of day content after its creation
    [HttpGet("{dayContentId:int}/products")]
    public async Task<ActionResult<ICollection<DayContentProductDetail>>> GetDayContentDetailAsync(int dayContentId)
    {
        try
        {
            // Your logic to retrieve day content products based on dayContentId 
            var dayContentProducts = await dayContentLogic.GetDayContentDetailAsync(dayContentId);

            return Ok(dayContentProducts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}