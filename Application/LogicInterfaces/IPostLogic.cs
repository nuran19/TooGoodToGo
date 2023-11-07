using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    
    //view post - search/list
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
  
  // view single post ??????
     Task<PostBasicDto> GetByIdAsync(int id);
}