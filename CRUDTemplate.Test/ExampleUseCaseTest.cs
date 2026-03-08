using System;
using CRUDTemplate.Domain.Interface;
using CRUDTemplate.Domain.Model;
using CRUDTemplate.Domain.UseCase;

namespace CRUDTemplate.Test;

/// <summary>
/// Tests for the <see cref="ExampleUseCase"/> class.
/// </summary>
public class ExampleUseCaseTest
     : BaseUseCaseTests<ExampleUseCase, InterfaceExampleRepository, ModelExample, int>
{
    /// <summary>
    /// Creates a use case instance for testing.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <returns>An <see cref="ExampleUseCase"/> instance.</returns>
    protected override ExampleUseCase CreateUseCase(InterfaceExampleRepository repository) => new ExampleUseCase(repository);

    /// <summary>
    /// Creates a valid model for testing.
    /// </summary>
    /// <returns>A valid <see cref="ModelExample"/> instance.</returns>
    protected override ModelExample CreateValidModel() => new ModelExample("valid") { Id = 1 };

    /// <summary>
    /// Creates an invalid model for testing.
    /// </summary>
    /// <returns>An invalid <see cref="ModelExample"/> instance.</returns>
    protected override ModelExample CreateInvalidModel() => new ModelExample(string.Empty);

    /// <summary>
    /// Gets the model ID.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>The ID.</returns>
    protected override int GetModelId(ModelExample model) => model.Id;
}