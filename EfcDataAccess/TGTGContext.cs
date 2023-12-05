using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;

namespace EfcDataAccess;

public class TGTGContext: DbContext
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
        modelBuilder.Entity<DayContent>().HasIndex(dc => dc.Date).IsUnique();
        modelBuilder.Entity<DayContentProduct>().HasKey(DayContentProduct => new
            { DayContentProduct.DayContentId, DayContentProduct.ProductId });

        

        //todo first add company
        
        /*modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new User()
            {
                Password = "qwert",
                UserName = "qwer",
                Role = "qwer",
                CompanyId = 1
            }
        });*/
    }
    
    
}