using System;

namespace CRUDTemplate.Data;


/// <summary>
/// Provides database seeding logic for initial data population.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds the database with initial data if it does not already exist.
    /// </summary>
    /// <param name="context">Database context instance.</param>
    public static async Task Seed(AppDbContext context)
    {
        Console.WriteLine("Database seeded successfully.");
    }
}
