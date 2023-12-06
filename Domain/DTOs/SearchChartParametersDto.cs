namespace Domain.DTOs;

public class SearchChartParametersDto
{ 
    public int CompanyId { get; }
    public string? CategoryContains { get; }
    public string? SubCategoryContains { get; }
    public string? ProductTypeContains { get;}
    public string? ReasonContains { get;}
    public int? DayQuantityContains { get; } 
    public DateOnly? Date { get; set; }

    public SearchChartParametersDto(int companyId, string? productTypeContains)
    {
        CompanyId = companyId;
        ProductTypeContains = productTypeContains;
    }
    
    public SearchChartParametersDto(int companyId, string? categoryContains, string? subCategoryContains, string? productTypeContains, string? reasonContains ,int? dayQuantityContains, DateOnly? date) //date and companyId are a must
    {
        CompanyId = companyId;
        CategoryContains = categoryContains;
        SubCategoryContains = subCategoryContains;
        ProductTypeContains = productTypeContains;
        ReasonContains = reasonContains;
        DayQuantityContains = dayQuantityContains;
        Date = date;
    }

    

}