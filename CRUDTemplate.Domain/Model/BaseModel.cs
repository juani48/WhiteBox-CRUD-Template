using System;
using System.ComponentModel.DataAnnotations;
using CRUDTemplate.Domain.Validator;

namespace CRUDTemplate.Domain.Model;

public abstract class BaseModel
{
    /// <summary>
    /// Gets or sets the identifier of the model.
    /// </summary>
    [Key] public int Id { get; set; }
    public BaseModel() { }
    public BaseModel(int id) { Id = id; }
    public static bool IsValid(BaseModel model) => new BaseModelValidator().Validate(model).IsValid;
}
