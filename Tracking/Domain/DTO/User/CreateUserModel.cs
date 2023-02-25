using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.User;

public class CreateUserModel
{
    [MinLength(1)]
    public string FirstName { get; set; } = null!;

    [MinLength(1)]
    public string LastName { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    //[DataType(DataType.Password)]
    [MinLength(5)]
    public string Password { get; set; } = null!;
    
    //[DataType(DataType.Password)]
    [MinLength(5)]
    [Compare(nameof(Password))]
    public string RetryPassword { get; set; } = null!;
}