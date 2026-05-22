using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Models;

namespace NetCoreTemplate.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : DbContext(options)
{
    public DbSet<CarModel> Cars => Set<CarModel>();
}