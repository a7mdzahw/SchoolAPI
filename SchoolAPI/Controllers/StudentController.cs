using BLL.Dtos;
using BLL.Helpers;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class StudentController : ControllerBase
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly PasswordUtils _passwordUtils;

    public StudentController(SchoolDbContext schoolDbContext,PasswordUtils passwordUtils)
    {
        _schoolDbContext = schoolDbContext;
        _passwordUtils = passwordUtils;
    }
    [HttpPost]
    public IActionResult Register(RegisterDto registerDto)
    {

        if (ModelState.IsValid)
        {
            var newStudent = new Student()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Age = registerDto.Age,
                Password = _passwordUtils.EncryptPassword(registerDto.Password)
            };
            
            _schoolDbContext.Students.Add(newStudent);
            _schoolDbContext.SaveChanges();

            return Ok(true);
        }
        else
        {
            return BadRequest("Invalid Data");
        }
        
    }

    [HttpGet,Authorize]
    public IActionResult? GetAllStudents()
    {
        return Ok(_schoolDbContext.Students.ToList());
    }
}