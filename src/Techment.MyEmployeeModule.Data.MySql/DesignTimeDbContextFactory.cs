using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Techment.MyEmployeeModule.Data.Repositories;

namespace Techment.MyEmployeeModule.Data.MySql;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyEmployeeModuleDbContext>
{
    public MyEmployeeModuleDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyEmployeeModuleDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=localhost;User=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseMySql(
            connectionString,
            ResolveServerVersion(args, connectionString),
            options => options.MigrationsAssembly(typeof(MySqlDataAssemblyMarker).Assembly.GetName().Name));

        return new MyEmployeeModuleDbContext(builder.Options);
    }

    private static ServerVersion ResolveServerVersion(string[] args, string connectionString)
    {
        var serverVersion = args.Length >= 2 ? args[1] : null;

        if (serverVersion == "AutoDetect")
        {
            return ServerVersion.AutoDetect(connectionString);
        }

        if (serverVersion != null)
        {
            return ServerVersion.Parse(serverVersion);
        }

        return new MySqlServerVersion(new Version(5, 7));
    }
}
