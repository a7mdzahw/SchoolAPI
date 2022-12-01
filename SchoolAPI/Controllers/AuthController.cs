using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Dtos;
using BLL.Helpers;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SchoolAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AuthController : ControllerBase
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IConfiguration _configuration;
    private readonly PasswordUtils _passwordUtils;

    public AuthController(SchoolDbContext schoolDbContext, IConfiguration configuration,PasswordUtils passwordUtils)
    {
        _schoolDbContext = schoolDbContext;
        _configuration = configuration;
        _passwordUtils = passwordUtils;
    }
    [HttpPost]
    public IActionResult Login(LoginDto loginDto)

    {
        var student = _schoolDbContext.Students.FirstOrDefault(s => s.Email == loginDto.Email);

        if (student is null)
        {
            return BadRequest("Not Found");
        }

        if ( _passwordUtils.DecryptPassword(student.Password) != loginDto.Password) return BadRequest("Error Password");

        var token = GenerateToken(student);

        return Ok(token);
    }

    private string GenerateToken(Student student)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, student.Email),
            new Claim(ClaimTypes.Name, student.Name)
        };
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));
        var signingCredential = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(claims: claims, signingCredentials: signingCredential,
            expires: DateTime.Now.AddMinutes(20));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}