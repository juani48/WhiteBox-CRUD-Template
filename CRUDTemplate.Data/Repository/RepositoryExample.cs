using System;
using CRUDTemplate.Domain.Interface;
using CRUDTemplate.Domain.Model;

namespace CRUDTemplate.Data.Repository;

/// <summary>
/// Repository implementation for <see cref="ModelExample"/> entities.
/// </summary>
public class RepositoryExample : BaseRepository<ModelExample, int>, InterfaceExampleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryExample"/> class.
    /// </summary>
    public RepositoryExample(AppDbContext db) : base("Not found", "Already exists", db) { }
    protected override int GetIdFromModel(ModelExample model) => model.Id;
}
