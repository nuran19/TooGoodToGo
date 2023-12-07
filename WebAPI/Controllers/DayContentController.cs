using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class DayContentController : ControllerBase
{
    private readonly IDayContentLogic dayContentLogic;

    public DayContentController(IDayContentLogic dayContentLogic)
    {
        this.dayContentLogic = dayContentLogic;
    }

    [HttpGet("month-entries")]
    public async Task<ActionResult<List<DayContent>>> GetMonthEntries([FromQuery] int month, [FromQuery] int year)
    {
        try
        {
            List<DayContent> entries = await dayContentLogic.GetMonthEntries(month, year);
            return Ok(entries);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("date-range-entries")]
    public async Task<ActionResult<List<DayContent>>> GetEntriesForDateRange([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        try
        {
            List<DayContent> entries = await dayContentLogic.GetEntriesForDateRange(startDate, endDate);
            return Ok(entries);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
