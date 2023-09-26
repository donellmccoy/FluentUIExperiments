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

    public DbSet<TypeOfWork> TypesOfWork
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

    public DbSet<FilterData> FilterData
    {
        get; set;
    }

    public int GetCountOfSomethingById(int id) => throw new NotImplementedException();

    public async Task<IReadOnlyList<FilterData>> GetFilterDataAsync(CancellationToken token = default)
    {
        return await FilterData
            .FromSqlRaw("SELECT * FROM [dbo].[ufnGetFilterData]()")
            .AsNoTracking()
            .ToListAsync(token);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(GetCountOfSomethingById)), builder =>
        {
            builder.HasParameter("id");
        });
    }
}