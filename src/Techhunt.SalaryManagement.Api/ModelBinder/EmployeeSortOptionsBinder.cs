using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Application;

namespace Techhunt.SalaryManagement.Api.ModelBinder
{
    public class EmployeeSortOptionsBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {            
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return AddModelError(bindingContext);
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var queryParts = bindingContext.HttpContext.Request.QueryString.Value.Split('=');

            var value = queryParts[queryParts.Length - 1];

            if (string.IsNullOrEmpty(value))
            {
                return AddModelError(bindingContext);
            }

            if (value.Length < 3 || value.Length > 7)
            {
                return AddModelError(bindingContext);
            }

            var model = new EmployeeSortOptions();

            var firstLetter = value[0];
            switch (firstLetter)
            {
                case '+':
                    model.Order = Order.Asc;
                    break;
                case '-':
                    model.Order = Order.Desc;
                    break;
                default:
                    return AddModelError(bindingContext);
            }

            var property = value.Substring(1).ToLower();
            switch (property)
            {
                case "id":
                    model.Field = Field.Id;
                    break;
                case "login":
                    model.Field = Field.Login;
                    break;
                case "name":
                    model.Field = Field.Name;
                    break;
                case "salary":
                    model.Field = Field.Salary;
                    break;
                default:
                    return AddModelError(bindingContext);
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        private Task AddModelError(ModelBindingContext bindingContext)
        {
            bindingContext.ModelState.AddModelError("sort", "Invalid sort parameter");
            return Task.CompletedTask;
        }
    }
}
