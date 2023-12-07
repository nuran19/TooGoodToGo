using System.Runtime.InteropServices.JavaScript;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Shared.Models;

namespace Application.Logic;

public class DayContentLogic : IDayContentLogic
{
    private readonly IDayContentDao dayContentDao;
    private readonly IDayContentProductDao dayContentProductDao;
    private readonly IUserDao userDao;

    public DayContentLogic(IDayContentDao dayContentDao, IUserDao userDao, IDayContentProductDao dayContentProductDao)
    {
        this.dayContentDao = dayContentDao;
        this.userDao = userDao;
        this.dayContentProductDao = dayContentProductDao;

    }

    public async Task<DayContent> CreateAsync(DayContentCreationDto dto)
    {
         
        DayContent dayContent = new DayContent(dto.OwnerId, dto.Date, dto.DayContentProducts);
        DayContent created = await dayContentDao.CreateAsync(dayContent);
        
        // Once the day content is created, then create its details using its generated id
        
        await dayContentProductDao.CreateAsync(created.Id,dto.DayContentProducts.ToList());
        
        return created;
    }

    private void ValidateDayContent(DayContentCreationDto dto)
    {
        if (dto.Date == default(DateOnly)) throw new Exception("Date can not be empty!");
        // other validation stuff
    }
    //View dayContents
    public Task<IEnumerable<DayContent>> GetAsync(SearchDayContentParametersDto searchParameters)
    {
        return dayContentDao.GetAsync(searchParameters);
    }

    public async Task<DayContentBasicDto> GetByIdAsync(int id)
    {
        DayContent? dayContent = await dayContentDao.GetByIdAsync(id);
        if (dayContent == null)
        {
            throw new Exception($"DayContent with id {id} not found.");
        }
        
        return new DayContentBasicDto(dayContent.Id, dayContent.Date);

    }
    
    public async Task<ICollection<DayContentProduct>> GetProductsAsync(int dayContentId)
    {
        try
        {
            var dayContentProducts = await dayContentProductDao.GetProductsAsync(dayContentId);

            return dayContentProducts;
        }
        catch (Exception e)
        {
            
            Console.WriteLine(e);
            throw; 
        }
    }

    public async Task<ICollection<DayContentProductDetail>> GetDayContentDetailAsync(int dayContentId)
    {
        try
        {
            //  Logic to retrieve day content products based on dayContentId
            var dayContentProducts = await dayContentProductDao.GetDayContentDetailAsync(dayContentId);

            return dayContentProducts;
        }
        catch (Exception e)
        { 
            Console.WriteLine(e); 
        }

        return null;
    }
    
    public DayContentLogic(IDayContentDao dayContentDao)
    {
        this.dayContentDao = dayContentDao;
    }

    public async Task<List<DayContent>> GetMonthEntries(int month, int year)
    {
        return await dayContentDao.GetMonthEntries(month, year);
    }
    
    public async Task<List<DayContent>> GetEntriesForDateRange(DateOnly startDate, DateOnly endDate)
    {
        return await dayContentDao.GetEntriesForDateRange(startDate, endDate);
    }
}