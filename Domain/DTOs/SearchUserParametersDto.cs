namespace Domain.DTOs;

public class SearchUserParametersDto
{
    public string? UsernameContains { get;  }
    public int? UserIdContains { get; }
    public int? CompanyIdContains { get; }
    public string? RoleContains { get;  }

//search user params constructers df
    public SearchUserParametersDto(string? usernameContains, int? userIdContains, int? companyIdContains, string? roleContains)
    {
        UsernameContains = usernameContains;
        UserIdContains = userIdContains;
        CompanyIdContains = companyIdContains;
        RoleContains = roleContains;
    }
}