using System.ComponentModel.DataAnnotations;

public class LoginUserDTO
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}