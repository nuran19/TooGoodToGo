using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Shared.Models;

namespace Application.Logic;

public class ChartLogic : IChartLogic
{
    private readonly IChartDao chartDao;

    public ChartLogic(IChartDao chartDao)
    {
        this.chartDao = chartDao;
    }

   public  Task<IEnumerable<DayContentProduct>> GetAsync(SearchChartParametersDto searchParameters)
    {
        return chartDao.GetAsync(searchParameters); 
}
    
   public  Task<IEnumerable<DayContentProduct>> GetAsyncMore(SearchChartParametersDto searchParameters)
   {
       return chartDao.GetAsync(searchParameters); 
   }
}