using System;
using CRUDTemplate.Domain.Interface;
using CRUDTemplate.Domain.Model;

namespace CRUDTemplate.Domain.UseCase;

/// <summary>
/// Use case implementation for managing <see cref="ModelExample"/> instances.
/// </summary>
public class ExampleUseCase : BaseUseCase<ModelExample, int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleUseCase"/> class.
    /// </summary>
    /// <param name="repository">
    /// Repository used to perform data operations for <see cref="ModelExample"/>.
    /// </param>
    public ExampleUseCase(InterfaceExampleRepository repository) : base(repository) { }

    /// <summary>
    /// Determines whether the specified model contains missing required data.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <returns>
    /// True if the model is considered invalid or empty; otherwise, false.
    /// </returns>
    protected override bool IsValid(ModelExample model) => ModelExample.IsValid(model);
}