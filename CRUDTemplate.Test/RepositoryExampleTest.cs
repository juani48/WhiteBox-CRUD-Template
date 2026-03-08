using System;
using CRUDTemplate.Data;
using CRUDTemplate.Data.Repository;
using CRUDTemplate.Domain.Model;
using CRUDTemplate.Test.Base;

namespace CRUDTemplate.Test;

/// <summary>
/// Tests for the <see cref="RepositoryExample"/> class.
/// </summary>
public class RepositoryExampleTest: BaseRepositoryTests<RepositoryExample, ModelExample, int>
{
    /// <summary>
    /// Creates a repository instance for testing.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <returns>A <see cref="RepositoryExample"/> instance.</returns>
    protected override RepositoryExample CreateRepository(AppDbContext db) => new RepositoryExample(db);

    /// <summary>
    /// Creates a model for testing.
    /// </summary>
    /// <returns>A <see cref="ModelExample"/> instance.</returns>
    protected override ModelExample CreateModel() => new ModelExample("test");

    /// <summary>
    /// Gets the entity ID from a model.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>The ID.</returns>
    protected override int GetEntityId(ModelExample model) => model.Id;

    /// <summary>
    /// Modifies a model for update tests.
    /// </summary>
    /// <param name="model">The model to modify.</param>
    protected override void ModifyModel(ModelExample model) => model.Name = "updated";
}