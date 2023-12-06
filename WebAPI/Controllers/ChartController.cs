using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ChartController : ControllerBase
{
    private readonly IChartLogic chartLogic;

    public ChartController( IChartLogic chartLogic)
    {
        this.chartLogic = chartLogic;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DayContentProduct>>> GetAsync([FromQuery] int companyId,[FromQuery] string? productTypeContains)
    {
        try
        {
            SearchChartParametersDto parameters = new(companyId, productTypeContains);
            var chartData = await chartLogic.GetAsync(parameters);
            return Ok(chartData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet("MoreCharts")]
    public async Task<ActionResult<IEnumerable<DayContentProduct>>> GetAsync([FromQuery] int companyId, [FromQuery] string? categoryContains,[FromQuery] string? subCategoryContains,[FromQuery] string? productTypeContains,[FromQuery] string? reasonContains ,[FromQuery] int? dayQuantityContains,[FromQuery] DateOnly? date)
    {
        try
        {
            SearchChartParametersDto parameters = new(companyId, categoryContains, subCategoryContains, productTypeContains, reasonContains, dayQuantityContains, date);
            var chartData = await chartLogic.GetAsync(parameters);
            return Ok(chartData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}