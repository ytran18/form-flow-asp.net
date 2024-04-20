using demo.Models;
using Microsoft.EntityFrameworkCore;

namespace demo.Data;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Users> users { get; set; }

    public DbSet<Forms> forms { get; set; }
}