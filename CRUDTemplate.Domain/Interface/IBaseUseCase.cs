using System;
using System.Linq.Expressions;

namespace CRUDTemplate.Domain.Interface;

/// <summary>
/// Defines a base contract for application use cases,
/// providing common operations for managing application models.
/// </summary>
/// <typeparam name="M">Application model or DTO type.</typeparam>
/// <typeparam name="TId">Identifier type of the model.</typeparam>
public interface IBaseUseCase<M, TId> where M : class
{
    /// <summary>
    /// Creates a new instance of the model.
    /// </summary>
    /// <param name="model">Model to be created.</param>
    /// <returns>The created model instance.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the model contains missing or invalid data.
    /// </exception>
    Task<M> Create(M model);

    /// <summary>
    /// Updates an existing model.
    /// </summary>
    /// <param name="model">Model containing the updated data.</param>
    Task Update(M model);

    /// <summary>
    /// Deletes a model by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the model to delete.</param>
    Task Delete(TId id);

    /// <summary>
    /// Retrieves a model by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the model.</param>
    /// <returns>
    /// The model instance if found; otherwise, null.
    /// </returns>
    Task<M> GetById(TId id);

    /// <summary>
    /// Retrieves a paginated list of models.
    /// </summary>
    /// <param name="page">Page number to retrieve.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <returns>List of models for the specified page.</returns>
    Task<List<M>> GetAll(int page, int pageSize);

    /// <summary>
    /// Retrieves a paginated list of models that match the specified predicate.
    /// </summary>
    /// <param name="predicate">Filter condition to apply to the models.</param>
    /// <param name="page">Page number to retrieve.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <returns>List of models that satisfy the predicate.</returns>
    Task<List<M>> GetAll(Expression<Func<M, bool>> predicate, int page, int pageSize);
}