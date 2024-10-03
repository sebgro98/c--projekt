using AuthApi.Models;
using Microsoft.EntityFrameworkCore;
namespace AuthApi.Data;

//klassen representerar den kontext som entity framwork core andv�der och extendar DbContext f�r att kunna
// hantera databas opertationer
public class ApplicationDbContext : DbContext
{
    //tom konsturkor s� kommer att anv�nda base options vilket betyder att DbContext kommer s�tta dem �t mig
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //H�r �r det som kommer representera mina tabeller och till�ter mig k�ra create, read, update, delete
    //quarries mot Users och Roles tabelen
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    //OnModelCreate till�ter mig att konfigurera databasen n�r modellen skapas s� att jag kan s�tta in v�rdena
    // Admin och User p� dess Id
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definiera roller
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "User" }
        );

    }
}
