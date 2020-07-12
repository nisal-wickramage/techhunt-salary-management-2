using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using Techhunt.SalaryManagement.Application;

namespace Techhunt.SalaryManagement.Api.ModelBinder
{
    public class EmployeeSortOptionsBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(EmployeeSortOptions))
            {
                return new BinderTypeModelBinder(typeof(EmployeeSortOptionsBinder));
            }

            return null;
        }
    }
}
