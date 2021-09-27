using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;

namespace MaximaTech.Domain.Validation
{
    public class PlatformGroupIdExistsAttribute : ValidationAttribute
    {
        private bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                o.GetType().IsGenericType &&
                o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                IDepartmentRepository departmentRepository = (IDepartmentRepository)validationContext.GetService(typeof(IDepartmentRepository));

                if (IsList(value))
                {
                    foreach (Guid id in (List<Guid>)value)
                    {
                        Department department = departmentRepository.FindById(id);

                        if (department == null)
                        {
                            return new ValidationResult("Invalid department");
                        }
                    }
                }
                else
                {
                    Department department = departmentRepository.FindById(Guid.Parse(value.ToString()));

                    if (department == null)
                    {
                        return new ValidationResult("Invalid department");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}