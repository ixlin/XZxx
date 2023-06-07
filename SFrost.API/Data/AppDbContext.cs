using System.Reflection.Emit;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ExcelData>()
        .Property(e => e.Id)
        .ValueGeneratedOnAdd();

        base.OnModelCreating(builder);
    }
    public DbSet<ExcelData> ExcelData { get; set; }

}
