using Domain.DTOs;
using Domain.Models;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IChartDao
{
    Task<IEnumerable<DayContentProduct>> GetAsync(SearchChartParametersDto searchParameters);
    Task<IEnumerable<DayContentProduct>> GetAsyncMore(SearchChartParametersDto searchParams);
}