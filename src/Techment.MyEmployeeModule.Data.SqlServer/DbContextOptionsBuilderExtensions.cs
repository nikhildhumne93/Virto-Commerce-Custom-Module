using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Platform.Data.SqlServer;

namespace Techment.MyEmployeeModule.Data.SqlServer;
public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseSqlServerDatabase(this DbContextOptionsBuilder builder, string connectionString) =>
            builder.UseSqlServer(connectionString, db => db
                .MigrationsAssembly(typeof(SqlServerDbContextFactory).Assembly.GetName().Name));
}

