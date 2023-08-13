using Microsoft.EntityFrameworkCore;

namespace FluentUIExperiments.Models;

public class DataCenterWorkflowContext : DbContext
{
    public DataCenterWorkflowContext(DbContextOptions<DataCenterWorkflowContext> options)
        : base(options)
    {
    }

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
    }
}