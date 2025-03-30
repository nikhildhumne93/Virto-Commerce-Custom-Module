using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Techment.MyEmployeeModule.Data.Models;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace Techment.MyEmployeeModule.Data.Repositories;

public class MyEmployeeModuleDbContext : DbContextBase
{
    public MyEmployeeModuleDbContext(DbContextOptions<MyEmployeeModuleDbContext> options)
        : base(options)
    {
    }

    protected MyEmployeeModuleDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EmployeeEntity>().ToTable("Employee").HasKey(x => x.Id);
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.FirstName).HasMaxLength(250);
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.LastName).HasMaxLength(250);
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.Gender).HasMaxLength(10);
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.Age).HasColumnType("int");
        modelBuilder.Entity<EmployeeEntity>().Property(x => x.Salary).HasColumnType("int");
        ;
        //modelBuilder.Entity<MyEmployeeModuleEntity>().ToTable("MyEmployeeModule").HasKey(x => x.Id);
        //modelBuilder.Entity<MyEmployeeModuleEntity>().Property(x => x.Id).HasMaxLength(IdLength).ValueGeneratedOnAdd();

        switch (Database.ProviderName)
        {
            case "Pomelo.EntityFrameworkCore.MySql":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Techment.MyEmployeeModule.Data.MySql"));
                break;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Techment.MyEmployeeModule.Data.PostgreSql"));
                break;
            case "Microsoft.EntityFrameworkCore.SqlServer":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Techment.MyEmployeeModule.Data.SqlServer"));
                break;
        }
    }
}
