using System;
using System.Data;
using CRUDTemplate.Domain.Model;
using FluentValidation;

namespace CRUDTemplate.Domain.Validator;

public class ModelExampleValidator: AbstractValidator<ModelExample>
{
    public ModelExampleValidator()
    {
        RuleFor(model => model).NotNull();
        RuleFor(model => model.Name).NotEmpty().MaximumLength(100);
    }
}
