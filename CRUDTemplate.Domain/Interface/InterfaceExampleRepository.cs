using System;
using CRUDTemplate.Domain.Model;

namespace CRUDTemplate.Domain.Interface;

/// <summary>
/// Defines a repository contract for <see cref="ModelExample"/> entities.
/// </summary>
public interface InterfaceExampleRepository: IBaseRepository<ModelExample, int> { }
