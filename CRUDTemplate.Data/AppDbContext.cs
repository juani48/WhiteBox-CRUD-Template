using System;
using CRUDTemplate.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUDTemplate.Data;

/// <summary>
/// Represents the Entity Framework Core database context for the application.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the examples table.
    /// </summary>
    public DbSet<ModelExample> Users { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
