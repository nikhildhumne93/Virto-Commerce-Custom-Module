using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Techment.MyEmployeeModule.Data.Repositories;

namespace Techment.MyEmployeeModule.Data.PostgreSql;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyEmployeeModuleDbContext>
{
    public MyEmployeeModuleDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyEmployeeModuleDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=localhost;Username=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseNpgsql(
            connectionString,
            options => options.MigrationsAssembly(typeof(PostgreSqlDataAssemblyMarker).Assembly.GetName().Name));

        return new MyEmployeeModuleDbContext(builder.Options);
    }
}
