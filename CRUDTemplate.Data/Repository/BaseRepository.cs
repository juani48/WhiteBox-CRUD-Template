using System;
using System.Linq.Expressions;
using CRUDTemplate.Data.CustomException;
using CRUDTemplate.Domain.Interface;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUDTemplate.Data.Repository;

/// <summary>
/// Provides a base implementation for repositories using Entity Framework Core,
/// encapsulating common CRUD operations and persistence logic.
/// </summary>
/// <typeparam name="M">Application model or DTO type.</typeparam>
/// <typeparam name="TId">Identifier type of the entity.</typeparam>
public abstract class BaseRepository<M, TId> : IBaseRepository<M, TId> where M : class
{
    private readonly AppDbContext _db;
    private readonly string _notFoundMessage;
    private readonly string _foundMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{M, TId}"/> class.
    /// </summary>
    /// <param name="notFoundMessage">Error message used when an entity is not found.</param>
    /// <param name="foundMessage">Error message used when a duplicate entity is detected.</param>
    /// <param name="db">Database context instance.</param>
    protected BaseRepository(string notFoundMessage, string foundMessage, AppDbContext db)
    {
        _db = db;
        _notFoundMessage = notFoundMessage;
        _foundMessage = foundMessage;
    }

    /// <summary>
    /// Gets the underlying <see cref="AppDbContext"/> instance.
    /// </summary>
    /// <returns>The database context.</returns>
    protected AppDbContext GetDbContext() => _db;

    /// <summary>
    /// Creates a new model in the data store.
    /// </summary>
    /// <param name="model">Model to be created.</param>
    /// <returns>The created model.</returns>
    /// <exception cref="DuplicateException">
    /// Thrown when an entity with the same identifier already exists.
    /// </exception>
    public async Task<M> Create(M model)
    {
        if (await Exists(GetIdFromModel(model))) throw new DuplicateException(_foundMessage);
        _db.Set<M>().Add(model);
        await _db.SaveChangesAsync();
        return model;
    }

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the entity to delete.</param>
    /// <exception cref="NotFoundException">
    /// Thrown when the entity does not exist.
    /// </exception>
    public async Task Delete(TId id)
    {
        M? model = await _db.Set<M>().FindAsync(id);
        if (model == null) throw new NotFoundException(_notFoundMessage);
        _db.Set<M>().Remove(model);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Determines whether an entity with the specified identifier exists.
    /// </summary>
    /// <param name="id">Identifier of the entity.</param>
    /// <returns>
    /// True if the entity exists; otherwise, false.
    /// </returns>
    public async Task<bool> Exists(TId id)
        => await Task.FromResult(_db.Set<M>().Find(id) != null);

    /// <summary>
    /// Retrieves a paginated list of models.
    /// </summary>
    /// <param name="page">Page number to retrieve.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <returns>List of models for the specified page.</returns>
    public async Task<List<M>> GetAll(int page, int pageSize)
        => await Task.FromResult(_db.Set<M>().Skip((page - 1) * pageSize).Take(pageSize).ToList());

    /// <summary>
    /// Retrieves a paginated list of models that match the specified predicate.
    /// </summary>
    /// <param name="predicate">Filter condition to apply.</param>
    /// <param name="page">Page number to retrieve.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <returns>List of models that satisfy the predicate.</returns>
    public async Task<List<M>> GetAll(Expression<Func<M, bool>> predicate, int page, int pageSize)
        => await Task.FromResult(_db.Set<M>().Where(predicate).Skip((page - 1) * pageSize).Take(pageSize).ToList());

    /// <summary>
    /// Retrieves a model by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the entity.</param>
    /// <returns>The corresponding model.</returns>
    /// <exception cref="NotFoundException">
    /// Thrown when the entity does not exist.
    /// </exception>
    public async Task<M> GetById(TId id)
    {
        M? model = await _db.Set<M>().FindAsync(id);
        if (model == null) throw new NotFoundException(_notFoundMessage);
        return model;
    }

    /// <summary>
    /// Updates an existing model in the data store.
    /// </summary>
    /// <param name="model">Model containing the updated data.</param>
    /// <returns>The updated model.</returns>
    /// <exception cref="NotFoundException">
    /// Thrown when the entity does not exist.
    /// </exception>
    public async Task<M> Update(M model)
    {
        M? existingModel = await _db.Set<M>().FindAsync(GetIdFromModel(model));
        if (existingModel == null) throw new NotFoundException(_notFoundMessage);
        EntityEntry<M> entry = _db.Entry(existingModel);
        foreach (IProperty property in entry.Metadata.GetProperties())
        {
            if (property.IsPrimaryKey()) continue;
            object? value = property.PropertyInfo?.GetValue(existingModel);
            entry.Property(property.Name).CurrentValue = value;
        }
        await _db.SaveChangesAsync();
        return existingModel;
    }

    /// <summary>
    /// Extracts the identifier value from the specified model.
    /// </summary>
    protected abstract TId GetIdFromModel(M model);
}
