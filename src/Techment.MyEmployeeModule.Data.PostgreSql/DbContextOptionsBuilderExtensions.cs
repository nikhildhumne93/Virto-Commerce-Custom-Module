using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Platform.Data.PostgreSql;

namespace Techment.MyEmployeeModule.Data.PostgreSql;
public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UsePostgreSqlDatabase(this DbContextOptionsBuilder builder, string connectionString) =>
            builder.UseNpgsql(connectionString, db => db
                .MigrationsAssembly(typeof(PostgreSqlDbContextFactory).Assembly.GetName().Name));

}
