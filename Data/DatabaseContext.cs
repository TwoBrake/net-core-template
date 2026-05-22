using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Models;

namespace NetCoreTemplate.Data;

public class DatabaseContext : DbContext
{
    public DbSet<CarModel> Cars => Set<CarModel>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}