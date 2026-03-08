using System;
using CRUDTemplate.Data;
using CRUDTemplate.Data.CustomException;
using CRUDTemplate.Data.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CRUDTemplate.Test.Base;

/// <summary>
/// Base class for repository tests, providing common test setup and assertions.
/// </summary>
/// <typeparam name="TRepository">The repository type to test.</typeparam>
/// <typeparam name="TModel">The model type.</typeparam>
/// <typeparam name="TId">The identifier type.</typeparam>
public abstract class BaseRepositoryTests<TRepository, TModel, TId>
    where TRepository : BaseRepository<TModel, TId>
    where TModel : class
{
    /// <summary>
    /// Gets or sets the database context used in tests.
    /// </summary>
    protected AppDbContext DbContext = null!;

    /// <summary>
    /// Gets or sets the repository instance under test.
    /// </summary>
    protected TRepository Repository = null!;

    /// <summary>
    /// Creates a repository instance for testing.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <returns>A repository instance.</returns>
    protected abstract TRepository CreateRepository(AppDbContext db);

    /// <summary>
    /// Creates a valid model for testing.
    /// </summary>
    /// <returns>A model instance.</returns>
    protected abstract TModel CreateModel();

    /// <summary>
    /// Gets the identifier from a model.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>The identifier.</returns>
    protected abstract TId GetEntityId(TModel model);

    /// <summary>
    /// Modifies a model for update tests.
    /// </summary>
    /// <param name="model">The model to modify.</param>
    protected abstract void ModifyModel(TModel model);

    /// <summary>
    /// Sets up the test environment.
    /// </summary>
    protected void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        DbContext = new AppDbContext(options);
        Repository = CreateRepository(DbContext);
    }

    [Fact]
    /// <summary>
    /// Tests that creating a model persists the entity.
    /// </summary>
    public async Task Create_ShouldPersistEntity()
    {
        Setup();
        TModel model = CreateModel();
        TModel result = await Repository.Create(model);
        DbContext.Set<TModel>().Should().HaveCount(1);
    }

    [Fact]
    /// <summary>
    /// Tests that creating a duplicate entity throws a DuplicateException.
    /// </summary>
    public async Task Create_WhenEntityExists_ShouldThrowDuplicateException()
    {
        Setup();
        TModel model = CreateModel();
        model = await Repository.Create(model);
        Func<Task> act = async () => await Repository.Create(model);
        await act.Should().ThrowAsync<DuplicateException>();
    }

    [Fact]
    /// <summary>
    /// Tests that getting a model by ID returns the model when it exists.
    /// </summary>
    public async Task GetById_WhenExists_ShouldReturnModel()
    {
        Setup();
        TModel model = CreateModel();
        model = await Repository.Create(model);
        TModel result = await Repository.GetById(GetEntityId(model));
        result.Should().NotBeNull();
    }

    [Fact]
    /// <summary>
    /// Tests that getting a model by ID throws NotFoundException when it does not exist.
    /// </summary>
    public async Task GetById_WhenNotExists_ShouldThrowNotFoundException()
    {
        Setup();
        Func<Task> act = async () => await Repository.GetById(default!);
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    /// <summary>
    /// Tests that updating a model updates the entity.
    /// </summary>
    public async Task Update_WhenExists_ShouldUpdateEntity()
    {
        Setup();
        TModel model = CreateModel();
        model = await Repository.Create(model);
        ModifyModel(model);
        TModel result = await Repository.Update(model);
        result.Should().NotBeNull();
    }

    [Fact]
    /// <summary>
    /// Tests that deleting a model removes the entity.
    /// </summary>
    public async Task Delete_WhenExists_ShouldRemoveEntity()
    {
        Setup();
        TModel model = CreateModel();
        model = await Repository.Create(model);
        await Repository.Delete(GetEntityId(model));
        DbContext.Set<TModel>().Should().BeEmpty();
    }
}