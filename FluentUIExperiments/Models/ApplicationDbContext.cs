using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FluentUIExperiments.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GetInitializationDataResult>(entity =>
        {
            entity.HasNoKey();
        });
    }

    public virtual DbSet<GetInitializationDataResult> GetEmployeesWithDepartmentResults
    {
        get; set;
    }

    public IEnumerable<GetInitializationDataResult> SP_GetInitializationData()
    {
        return GetEmployeesWithDepartmentResults.FromSqlInterpolated($"[dbo].[USP_GET_INITIALIZATION_DATA_1]").ToArray();
    }
}