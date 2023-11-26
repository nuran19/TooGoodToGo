namespace Domain.DTOs;

public class ProductBasicDto
{
    public int Id { get; }
    public string OwnerName { get; }
    public string CategoryName { get; }
  public string SubCategoryName { get; }
    public string Type { get; }
    public string Brand { get;  }
    public double Qty { get; }

    
    public ProductBasicDto(int id,string ownerName,string categoryName, string subCategoryName ,string type, string brand, double qty) // todo add cat
    {
        Id = id;
       OwnerName = ownerName;
       CategoryName = categoryName;
       SubCategoryName = subCategoryName;
        Type = type;
        Brand = brand;
        Qty = qty;
    }
}