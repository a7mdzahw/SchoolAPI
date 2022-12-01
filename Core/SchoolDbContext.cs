using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Core;

public class SchoolDbContext: DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    { }

    public DbSet<Student> Students { get; set; }
}