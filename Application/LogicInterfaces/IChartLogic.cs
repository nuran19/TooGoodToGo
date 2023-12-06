using Domain.DTOs;
using Domain.Models;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IChartLogic
{
    Task<IEnumerable<DayContentProduct>> GetAsync(SearchChartParametersDto searchParameters);
    Task<IEnumerable<DayContentProduct>> GetAsyncMore(SearchChartParametersDto searchParameters);
}