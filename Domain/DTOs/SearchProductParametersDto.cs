namespace Domain.DTOs;

public class SearchProductParametersDto
{
    
    public string? Username { get;}
    public int? UserId { get;}
    public int? CompanyId { get; }
    public string? SubCategoryContains { get; }
    public string? TypeContains { get;}
    public string? BrandContains { get; }
    public int? DayContentId { get;  }
    
    
    public SearchProductParametersDto (string? username, int? userId, int? companyId, string? subCategoryContains, string? typeContains, string? brandContains, int? dayContentId = null)
    {
        Username = username;
        UserId = userId;
        CompanyId = companyId;
        SubCategoryContains = subCategoryContains;
        TypeContains = typeContains;
        BrandContains = brandContains;
        DayContentId = dayContentId;
    }
    
}