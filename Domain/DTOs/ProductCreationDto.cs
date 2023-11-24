using System.Xml;

namespace Domain.DTOs;

public class ProductCreationDto
{
    
    public int OwnerId { get; }
    public int CompanyId { get; }
    public int SubCategoryId { get; }
    public string Type { get; }
    public string Brand { get;}
    public int Qty { get;  }
    

    public ProductCreationDto(int ownerId,int companyId, int subCategoryId, string type, string brand, int qty)
    {
        OwnerId = ownerId;
        CompanyId = companyId;
        SubCategoryId = subCategoryId;//?????
        Type = type;
        Brand = brand;
        Qty = qty;
    }
    }
