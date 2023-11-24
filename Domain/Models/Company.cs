using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Company(string name)
    {
        Name = name;
    }
}