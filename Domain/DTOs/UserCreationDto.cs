namespace Domain.DTOs;
//a new DTO with just the properties needed to create a new User.
public class UserCreationDto
{
    public string UserName { get;}
public string Password { get; }
public int CompanyId { get; }
public string Role { get; }

    public UserCreationDto(string userName, string password, int companyId, string role)
    {
        UserName = userName;
        Password = password;
        CompanyId = companyId;
        Role = role;
    }
}