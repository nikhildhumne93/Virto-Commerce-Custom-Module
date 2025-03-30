using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Techment.MyEmployeeModule.Data.Repositories;

namespace Techment.MyEmployeeModule.Data.SqlServer;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyEmployeeModuleDbContext>
{
    public MyEmployeeModuleDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyEmployeeModuleDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=(local);User=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly(typeof(SqlServerDataAssemblyMarker).Assembly.GetName().Name));

        return new MyEmployeeModuleDbContext(builder.Options);
    }
}
