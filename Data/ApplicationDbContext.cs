using AuthApi.Models;
using Microsoft.EntityFrameworkCore;
namespace AuthApi.Data;

//klassen representerar den kontext som entity framwork core andväder och extendar DbContext för att kunna
// hantera databas opertationer
public class ApplicationDbContext : DbContext
{
    //tom konsturkor så kommer att använda base options vilket betyder att DbContext kommer sätta dem åt mig
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //Här är det som kommer representera mina tabeller och tillåter mig köra create, read, update, delete
    //quarries mot Users och Roles tabelen
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    //OnModelCreate tillåter mig att konfigurera databasen när modellen skapas så att jag kan sätta in värdena
    // Admin och User på dess Id
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definiera roller
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "User" }
        );

    }
}
