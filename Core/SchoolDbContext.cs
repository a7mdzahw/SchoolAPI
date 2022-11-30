using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Core;

public class SchoolDbContext: DbContext
{
    public SchoolDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Student> Students;
}