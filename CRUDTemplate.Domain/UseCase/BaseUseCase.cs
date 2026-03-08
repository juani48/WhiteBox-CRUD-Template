using System;
using System.Linq.Expressions;
using CRUDTemplate.Domain.Interface;

namespace CRUDTemplate.Domain.UseCase;

/// <summary>
/// Provides a base implementation for application use cases.
/// </summary>
/// <typeparam name="M">Application model or DTO type.</typeparam>
/// <typeparam name="TId">Identifier type.</typeparam>
public abstract class BaseUseCase<M, TId> : IBaseUseCase<M, TId> where M : class
{
    protected readonly IBaseRepository<M, TId> Repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseUseCase{M, TId}"/> class.
    /// </summary>
    /// <param name="repository">Repository used to perform data operations.</param>
    protected BaseUseCase(IBaseRepository<M, TId> repository)
    {
        Repository = repository;
    }

    /// <summary>
    /// Creates a new instance of the model.
    /// </summary>
    /// <param name="model">Model to be created.</param>
    /// <returns>The created model instance.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the model contains invalid data.
    /// </exception>
    public async Task<M> Create(M model)
    {
        if (!IsValid(model))
        {
            throw new ArgumentException("Invalid model data.");
        }
        return await Repository.Create(model);
    }

    /// <summary>
    /// Updates an existing model.
    /// </summary>
    /// <param name="model">Model containing the updated data.</param>
    public async Task Update(M model)
    {
        await Repository.Update(model);
    }

    /// <summary>
    /// Deletes a model by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the model to delete.</param>
    public async Task Delete(TId id)
    {
        await Repository.Delete(id);
    }

    /// <summary>
    /// Retrieves a model by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the model.</param>
    /// <returns>The model instance if found; otherwise, null.</returns>
    public async Task<M> GetById(TId id)
    {
        return await Repository.GetById(id);
    }

    /// <summary>
    /// Retrieves a paginated list of models.
    /// </summary>
    /// <param name="page">Page number to retrieve. Defaults to 1.</param>
    /// <param name="pageSize">Number of items per page. Defaults to 10.</param>
    /// <returns>List of models for the specified page.</returns>
    public async Task<List<M>> GetAll(int page = 1, int pageSize = 10)
    {
        return await Repository.GetAll(page, pageSize);
    }

    /// <summary>
    /// Retrieves a paginated list of models that match the specified predicate.
    /// </summary>
    /// <param name="predicate">Filter condition to apply to the models.</param>
    /// <param name="page">Page number to retrieve. Defaults to 1.</param>
    /// <param name="pageSize">Number of items per page. Defaults to 10.</param>
    /// <returns>List of models that satisfy the predicate.</returns>
    public async Task<List<M>> GetAll(Expression<Func<M, bool>> predicate, int page = 1, int pageSize = 10)
    {
        return await Repository.GetAll(predicate, page, pageSize);
    }

    /// <summary>
    /// Determines whether the specified model contains invalid data.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <returns>
    /// True if the model is considered invalid or empty; otherwise, false.
    /// </returns>
    protected abstract bool IsValid(M model);
}