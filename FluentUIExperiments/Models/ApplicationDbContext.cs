using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

    public virtual DbSet<FilterData> FilterData
    {
        get; set;
    }

    public int GetCountOfSomethingById(int id) => throw new NotImplementedException();

    public async Task<List<FilterData>> GetFilterDataAsync(CancellationToken token = default)
    {
        return await FilterData.FromSqlInterpolated($"[dbo].[uspGetFilterData]").ToListAsync(token);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //configuration for calling a user-defined scalar valued function
        modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(GetCountOfSomethingById)), builder =>
        {
            builder.HasParameter("id");
        });

        modelBuilder.Entity<FilterData>(entity => entity.HasNoKey());
    }
}