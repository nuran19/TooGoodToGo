using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class TGTGContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

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

        //todo first add company
        
        modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new User()
            {
                Password = "qwert",
                UserName = "qwer",
                Role = "qwer",
                CompanyId = 1
            }
        });
    }
}