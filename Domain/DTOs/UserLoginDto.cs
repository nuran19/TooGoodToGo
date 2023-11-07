namespace Domain.DTOs;

public class UserLoginDto
{
    public string Username { get; init; }  //init =can only set this values, when the object is created, but not later modify it
    public string Password { get; init; }
}