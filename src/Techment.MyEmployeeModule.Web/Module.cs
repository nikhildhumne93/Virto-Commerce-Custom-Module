using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.Platform.Data.MySql.Extensions;
using VirtoCommerce.Platform.Data.PostgreSql.Extensions;
using VirtoCommerce.Platform.Data.SqlServer.Extensions;
using Techment.MyEmployeeModule.Core;
using Techment.MyEmployeeModule.Data.MySql;
using Techment.MyEmployeeModule.Data.PostgreSql;
using Techment.MyEmployeeModule.Data.Repositories;
using Techment.MyEmployeeModule.Data.SqlServer;
using System;
using VirtoCommerce.Platform.Core.GenericCrud;
using Techment.MyEmployeeModule.Core.Models;
using Techment.MyEmployeeModule.Data.Services.Crud;
using Techment.MyEmployeeModule.Data.Services.Search;

namespace Techment.MyEmployeeModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<MyEmployeeModuleDbContext>(options =>
        {
            var databaseProvider = Configuration.GetValue("DatabaseProvider", "SqlServer");
            var connectionString = Configuration.GetConnectionString(ModuleInfo.Id) ?? Configuration.GetConnectionString("VirtoCommerce");

            switch (databaseProvider)
            {
                case "MySql":
                    options.UseMySqlDatabase(connectionString, typeof(MySqlDataAssemblyMarker), Configuration);
                    break;
                case "PostgreSql":
                    options.UsePostgreSqlDatabase(connectionString, typeof(PostgreSqlDataAssemblyMarker), Configuration);
                    break;
                default:
                    options.UseSqlServerDatabase(connectionString, typeof(SqlServerDataAssemblyMarker), Configuration);
                    break;
            }
        });
        serviceCollection.AddTransient<IMyNewEmployeeAppRepository, MyNewEmployeeAppRepository>();
        serviceCollection.AddTransient<Func<IMyNewEmployeeAppRepository>>(provider => () => provider.CreateScope().ServiceProvider.GetRequiredService<IMyNewEmployeeAppRepository>());
        serviceCollection.AddTransient<ICrudService<Employee>, EmployeeService>();
        serviceCollection.AddTransient<ISearchService<EmployeeSearchCriteria, EmployeeSearchResult, Employee>, EmployeeSearchService>();


        // Override models
        //AbstractTypeFactory<OriginalModel>.OverrideType<OriginalModel, ExtendedModel>().MapToType<ExtendedEntity>();
        //AbstractTypeFactory<OriginalEntity>.OverrideType<OriginalEntity, ExtendedEntity>();

        // Register services
        //serviceCollection.AddTransient<IMyService, MyService>();
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        var serviceProvider = appBuilder.ApplicationServices;

        // Register settings
        var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>();
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

        // Register permissions
        var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
        permissionsRegistrar.RegisterPermissions(ModuleInfo.Id, "MyEmployeeModule", ModuleConstants.Security.Permissions.AllPermissions);

        // Apply migrations
        using var serviceScope = serviceProvider.CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<MyEmployeeModuleDbContext>();
        dbContext.Database.Migrate();
    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
