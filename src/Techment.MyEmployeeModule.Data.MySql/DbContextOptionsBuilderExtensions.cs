using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Platform.Data.MySql;

namespace Techment.MyEmployeeModule.Data.MySql;
public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseMySqlDatabase(this DbContextOptionsBuilder builder, string connectionString) =>
          builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), db => db
              .MigrationsAssembly(typeof(MySqlDbContextFactory).Assembly.GetName().Name));


}
