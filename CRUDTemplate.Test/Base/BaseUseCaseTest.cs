using Moq;
using CRUDTemplate.Domain.Interface;
using CRUDTemplate.Domain.UseCase;
using FluentAssertions;

namespace CRUDTemplate.Test;

/// <summary>
/// Base class for use case tests, providing common test setup and assertions.
/// </summary>
/// <typeparam name="TUseCase">The use case type to test.</typeparam>
/// <typeparam name="TRepository">The repository type.</typeparam>
/// <typeparam name="TModel">The model type.</typeparam>
/// <typeparam name="TId">The identifier type.</typeparam>
public abstract class BaseUseCaseTests<TUseCase, TRepository, TModel, TId>
    where TUseCase : BaseUseCase<TModel, TId>
    where TRepository : class, IBaseRepository<TModel, TId>
    where TModel : class
{
    /// <summary>
    /// Gets the mocked repository.
    /// </summary>
    protected Mock<TRepository> RepositoryMock { get; private set; } = null!;

    /// <summary>
    /// Gets the use case instance under test.
    /// </summary>
    protected TUseCase UseCase { get; private set; } = null!;

    /// <summary>
    /// Creates a use case instance for testing.
    /// </summary>
    /// <param name="repository">The repository mock.</param>
    /// <returns>A use case instance.</returns>
    protected abstract TUseCase CreateUseCase(TRepository repository);

    /// <summary>
    /// Creates a valid model for testing.
    /// </summary>
    /// <returns>A valid model instance.</returns>
    protected abstract TModel CreateValidModel();

    /// <summary>
    /// Creates an invalid model for testing.
    /// </summary>
    /// <returns>An invalid model instance.</returns>
    protected abstract TModel CreateInvalidModel();

    /// <summary>
    /// Gets the identifier from a model.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>The identifier.</returns>
    protected abstract TId GetModelId(TModel model);

    /// <summary>
    /// Sets up the test environment.
    /// </summary>
    protected void Setup()
    {
        RepositoryMock = new Mock<TRepository>();
        UseCase = CreateUseCase(RepositoryMock.Object);
    }

    [Fact]
    /// <summary>
    /// Tests that creating an invalid model throws ArgumentException.
    /// </summary>
    public async Task Create_WhenModelIsInvalid_ShouldThrowArgumentException()
    {
        Setup();
        var model = CreateInvalidModel();

        Func<Task> act = async () => await UseCase.Create(model);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    /// <summary>
    /// Tests that creating a valid model calls the repository.
    /// </summary>
    public async Task Create_WhenModelIsValid_ShouldCallRepository()
    {
        Setup();
        var model = CreateValidModel();

        RepositoryMock
            .Setup(r => r.Create(model))
            .ReturnsAsync(model);

        var result = await UseCase.Create(model);

        result.Should().Be(model);
        RepositoryMock.Verify(r => r.Create(model), Times.Once);
    }

    [Fact]
    /// <summary>
    /// Tests that deleting calls the repository.
    /// </summary>
    public async Task Delete_ShouldCallRepository()
    {
        Setup();
        var id = GetModelId(CreateValidModel());

        await UseCase.Delete(id);

        RepositoryMock.Verify(r => r.Delete(id), Times.Once);
    }

    [Fact]
    /// <summary>
    /// Tests that getting by ID calls the repository.
    /// </summary>
    public async Task GetById_ShouldCallRepository()
    {
        Setup();
        var model = CreateValidModel();
        var id = GetModelId(model);

        RepositoryMock
            .Setup(r => r.GetById(id))
            .ReturnsAsync(model);

        var result = await UseCase.GetById(id);

        result.Should().Be(model);
        RepositoryMock.Verify(r => r.GetById(id), Times.Once);
    }
}
