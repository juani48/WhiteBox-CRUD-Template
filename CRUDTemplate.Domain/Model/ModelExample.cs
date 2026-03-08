using System;
using System.ComponentModel.DataAnnotations;
using CRUDTemplate.Domain.Validator;

namespace CRUDTemplate.Domain.Model;

/// <summary>
/// Represents an example application model.
/// </summary>
public class ModelExample: BaseModel
{
    /// <summary>
    /// Gets or sets the name of the model.
    /// </summary>
    [StringLength(100)] public string Name { get; set; } = string.Empty;
    public ModelExample(): base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelExample"/> class.
    /// </summary>
    /// <param name="name">Name associated with the model.</param>
    public ModelExample(string name): this() { Name = name; }

    public ModelExample(int id, string name) : base(id) { Name = name; }

    /// <summary>
    /// Determines whether the specified model contains invalid data.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <returns>
    /// True if the model is considered empty or invalid; otherwise, false.
    /// </returns>
    public static bool IsValid(ModelExample model) => new ModelExampleValidator().Validate(model).IsValid;
}