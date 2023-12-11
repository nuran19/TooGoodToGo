using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;

namespace EfcDataAccess;

public class TGTGContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<DayContent> DayContent { get; set; }
    public DbSet<SubCategory> SubCategory { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<DayContentProduct> DayContentProduct { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlite("Data Source = Product.db");  //initial 
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/TGTG.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    }

    //define primary keys, (and maybe constraints if not in logic)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(post => post.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Company>().HasKey(company => company.Id);
        modelBuilder.Entity<SubCategory>().HasKey(subCategory => subCategory.Id);
        modelBuilder.Entity<Category>().HasKey(Category => Category.Id);
        // modelBuilder.Entity<DayContent>().HasIndex(dc => dc.Date).IsUnique();
        modelBuilder.Entity<DayContentProduct>().HasKey(DayContentProduct => new
            { DayContentProduct.DayContentId, DayContentProduct.ProductId });



        //todo first add company

        Company c = null;
        modelBuilder.Entity<Company>().HasData(new List<Company>()
        {
            new Company("Rema1000")
            {
                Id = 1
            },
            new Company("Netto")
            {
                Id = 2
            }
        });


        modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new User()
            {
                Id = 10,
                Password = "password",
                UserName = "Alex",
                Role = "Manager",
                CompanyId = 1
            },
            new User()
            {
                Id = 11,
                Password = "password",
                UserName = "Maya",
                Role = "Employee",
                CompanyId = 1
            },
            new User()
            {
                Id = 12,
                Password = "password",
                UserName = "Mary",
                Role = "Employee",
                CompanyId = 2
            },
            new User()
            {
                Id = 13,
                Password = "password",
                UserName = "Luke",
                Role = "Manager",
                CompanyId = 2
            },
        });


        modelBuilder.Entity<Category>().HasData(new List<Category>()
        {
            new Category("Fruits")
            {
                Name = "Fruits",
                Id = 1
            },
            new Category("Vegetables")
            {
                Name = "Vegetables",
                Id = 2
            },
            new Category("Dairy")
            {
                Name = "Dairy",
                Id = 3
            },
            new Category("Meat and protein")
            {
                Name = "Meat and protein",
                Id = 4
            },
            new Category("Bakery")
            {
                Name = "Bakery",
                Id = 5
            },
            new Category("Grains")
            {
                Name = "Grains",
                Id = 6
            },
            new Category("Canned and Packaged Goods")
            {
                Name = " Canned and Packaged Goods",
                Id = 7
            },
            new Category("Frozen Foods")
            {
                Name = " Frozen Foods",
                Id = 8
            },
            new Category("Hot Foods")
            {
                Name = " Hot Foods",
                Id = 9
            },
            new Category("Snacks")
            {
                Name = "Snacks",
                Id = 10
            },
            new Category("Beverages")
            {
                Name = "Beverages",
                Id = 11
            }

        });

        modelBuilder.Entity<SubCategory>().HasData(new List<SubCategory>()
        {
            new SubCategory("Exotic fruits")
            {
                Id = 1,
                CategoryId = 1 // Reference to the Category 
            },
            new SubCategory("Berries")
            {
                Id = 2,
                CategoryId = 1 // Reference to the Category 
            },
            new SubCategory("Tropical fruits")
            {
                Id = 3,
                CategoryId = 1 // Reference to the Category 
            },
            new SubCategory("Stone fruits")
            {
                Id = 4,
                CategoryId = 1 // Reference to the Category 
            },
            new SubCategory("Leafy greens")
            {
                Id = 5,
                CategoryId = 2 // Reference to the Category 
            },
            new SubCategory("Root vegetables")
            {
                Id = 6,
                CategoryId = 2 // Reference to the Category 
            },
            new SubCategory("Cruciferous vegetables")
            {
                Id = 7,
                CategoryId = 2 // Reference to the Category 
            },
            new SubCategory("Nightshade vegetables")
            {
                Id = 8,
                CategoryId = 2 // Reference to the Category 
            },
            new SubCategory("Milk")
            {
                Id = 9,
                CategoryId = 3 // Reference to the Category 
            },
            new SubCategory("Cheese")
            {
                Id = 10,
                CategoryId = 3 // Reference to the Category 
            },
            new SubCategory("Yogurt")
            {
                Id = 11,
                CategoryId = 3 // Reference to the Category 
            },
            new SubCategory("Other..")
            {
                Id = 12,
                CategoryId = 3 // Reference to the Category 
            },
            new SubCategory("Poultry")
            {
                Id = 13,
                CategoryId = 4 // Reference to the Category 
            },
            new SubCategory("Beef")
            {
                Id = 14,
                CategoryId = 4 // Reference to the Category 
            },
            new SubCategory("Pork")
            {
                Id = 15,
                CategoryId = 4 // Reference to the Category 
            },
            new SubCategory("Seafood")
            {
                Id = 16,
                CategoryId = 4 // Reference to the Category 
            },
            new SubCategory("Deli meats")
            {
                Id = 17,
                CategoryId = 4 // Reference to the Category 
            },
            new SubCategory("Bread")
            {
                Id = 18,
                CategoryId = 5 // Reference to the Category 
            },
            new SubCategory("Pastries")
            {
                Id = 19,
                CategoryId = 5 // Reference to the Category 
            },
            new SubCategory("Rice")
            {
                Id = 20,
                CategoryId = 6 // Reference to the Category 
            },
            new SubCategory("Pasta")
            {
                Id = 21,
                CategoryId = 6 // Reference to the Category 
            },
            new SubCategory("Cereals")
            {
                Id = 22,
                CategoryId = 6 // Reference to the Category 
            },
            new SubCategory("Soups")
            {
                Id = 23,
                CategoryId = 7 // Reference to the Category 
            },
            new SubCategory("Sauces")
            {
                Id = 24,
                CategoryId = 7 // Reference to the Category 
            },
            new SubCategory("Canned vegetables and meat")
            {
                Id = 25,
                CategoryId = 8 // Reference to the Category 
            },
            new SubCategory("Vegetables and fruits")
            {
                Id = 26,
                CategoryId = 8 // Reference to the Category 
            },
            new SubCategory("Ready-made meals")
            {
                Id = 27,
                CategoryId = 8 // Reference to the Category 
            },
            new SubCategory("Ice cream")
            {
                Id = 28,
                CategoryId = 8 // Reference to the Category 
            },
            new SubCategory("Ready-made meals")
            {
                Id = 29,
                CategoryId = 9 // Reference to the Category 
            },
            new SubCategory("Salty snacks")
            {
                Id = 30,
                CategoryId = 10 // Reference to the Category 
            },
            new SubCategory("Sweet snacks")
            {
                Id = 31,
                CategoryId = 10 // Reference to the Category 
            },
            new SubCategory("Cold beverages")
            {
                Id = 32,
                CategoryId = 11 // Reference to the Category 
            },
            new SubCategory("Hot beverages")
            {
                Id = 33,
                CategoryId = 11 // Reference to the Category 
            },

        });

        modelBuilder.Entity<Product>().HasData(new List<Product>()
        {
            new Product(10, 1, 17, "Salami", "TastyTrades", 80)
            {
                Id = 1,
                OwnerId = 10, // Reference to a User 
                CompanyId = 1, // Reference to a Company
                SubCategoryId = 17 // Reference to the subcategory
            },
            new Product(11, 1, 12, "Eggs", "HarvestHaven", 500)
            {
                Id = 2,
                OwnerId = 11, // Reference to a User 
                CompanyId = 1, // Reference to a Company
                SubCategoryId = 12, // Reference to the subcategory
            },
            new Product(12, 2, 9, "Almond Milk", "Arla", 500)
            {
                Id = 3,
                OwnerId = 12, // Reference to a User 
                CompanyId = 2, // Reference to a Company
                SubCategoryId = 9 // Reference to the subcategory
            },
            new Product(13, 2, 10, "Cheddar", "DairyDelight", 250)
            {
                Id = 4,
                OwnerId = 13,
                CompanyId = 2,
                SubCategoryId = 10
            },
            /// add fruits vegg 

            new Product(14, 1, 1, "Banana", "FreshFruits", 500)
            {
                Id = 5,
                OwnerId = 10,
                CompanyId = 1,
                SubCategoryId = 1
            },
            new Product(15, 1, 2, "Blueberries", "BerryBoutique", 150)
            {
                Id = 6,
                OwnerId = 11,
                CompanyId = 1,
                SubCategoryId = 2
            },
            new Product(16, 1, 3, "Pineapple", "TropicalTreats", 1000)
            {
                Id = 7,
                OwnerId = 10,
                CompanyId = 1,
                SubCategoryId = 3
            },
            new Product(17, 1, 4, "Peach", "PeachyProduce", 300)
            {
                Id = 8,
                OwnerId = 11,
                CompanyId = 1,
                SubCategoryId = 4
            },

            // Vegetables
            new Product(18, 2, 5, "Spinach", "GreenGrove", 200)
            {
                Id = 9,
                OwnerId = 12,
                CompanyId = 2,
                SubCategoryId = 5
            },
            new Product(19, 2, 6, "Carrot", "RootyVeggies", 500)
            {
                Id = 10,
                OwnerId = 13,
                CompanyId = 2,
                SubCategoryId = 6
            },
            new Product(20, 2, 7, "Broccoli", "CruciferousCrops", 300)
            {
                Id = 11,
                OwnerId = 12,
                CompanyId = 2,
                SubCategoryId = 7
            },
            new Product(21, 2, 8, "Tomato", "TastyTomatoes", 400)
            {
                Id = 12,
                OwnerId = 13,
                CompanyId = 2,
                SubCategoryId = 8
            }
        });
    }
}
