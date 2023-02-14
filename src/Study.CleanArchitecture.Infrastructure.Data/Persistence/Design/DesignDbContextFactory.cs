using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Study.CleanArchitecture.Infrastructure.Data.Persistence.Design;

/// <summary>
/// 迁移需要
/// </summary>
public class DesignDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private const string ConnectionString =
        "Server=localhost;port=3306;database=clean_architecture;user id=root;password=123456;minimumpoolsize=10;maximumpoolsize=200;";

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}