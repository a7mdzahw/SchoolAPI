using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos;

public class RegisterDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Name { get; set; }

    public int Age { get; set; }
}