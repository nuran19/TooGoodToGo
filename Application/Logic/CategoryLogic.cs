using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class CategoryLogic:ICategoryLogic
{
    private readonly ICategoryDao catDao;
    

    public CategoryLogic(ICategoryDao catDao)  //User DAO is received through constructor dependency injection
    {
        this.catDao = catDao;
    }
    
    public Task<IEnumerable<Category>> GetAsync( )
    {
        return catDao.GetAsync();
    }
}