using System;
using CRUDTemplate.Domain.Model;
using FluentValidation;

namespace CRUDTemplate.Domain.Validator;

public class BaseModelValidator : AbstractValidator<BaseModel>
{
    public BaseModelValidator()
    {
        RuleFor(model => model).NotNull();
    }
}
