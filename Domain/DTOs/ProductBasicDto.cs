namespace Domain.DTOs;

public class ProductBasicDto
{
    public int Id { get; }
    public string OwnerName { get; }
  public string SubCategoryName { get; }
    public string Type { get; }
    public string Brand { get;  }
    public double Qty { get; }

    
    public ProductBasicDto(int id,string ownerName,string subCategoryName ,string type, string brand, double qty)
    {
        Id = id;
       OwnerName = ownerName;
       SubCategoryName = subCategoryName;
        Type = type;
        Brand = brand;
        Qty = qty;
    }
}