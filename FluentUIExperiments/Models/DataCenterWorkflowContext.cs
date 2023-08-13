using System.Windows.Automation.Provider;
using Microsoft.EntityFrameworkCore;

namespace FluentUIExperiments.Models;

public class DataCenterWorkflowContext : DbContext
{
    public DbSet<TypeOfWork> TypeOfWorks
    {
        get; set;
    }
    public DbSet<TypeOfInstrument> TypeOfInstruments
    {
        get; set;
    }

    public DbSet<County> Counties
    {
        get; set;
    }

    public DbSet<TypeOfCountBy> TypeOfCountBys
    {
        get; set;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DataCenterWorkflow", builder =>
        {
            builder.EnableRetryOnFailure(3);
        });

        optionsBuilder.UseLazyLoadingProxies();
    }
}