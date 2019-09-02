using CustomerList.Data.Mapping;
using CustomerList.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerList.Data
{
  public class ApplicationContext : DbContext
  {
    private readonly ILogger<ApplicationContext> _logger;

    public DbSet<User> User { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, ILogger<ApplicationContext> logger)
      : base(options)
    {
      _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      _logger.Log(LogLevel.Information, "OnModelCreating...");
      modelBuilder.ApplyConfiguration(new UserConfig());
    }
  }
}
