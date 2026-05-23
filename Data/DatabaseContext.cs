using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Models;

namespace NetCoreTemplate.Data;

/// <summary>
/// The context of our database.
/// </summary>
public class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : DbContext(options)
{
    public DbSet<CarModel> Cars => Set<CarModel>();
}