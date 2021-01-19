using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using TestProject.Common.Exception;

namespace TestProject.API.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!context.ModelState.IsValid)
            {
                foreach (var err in context.ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    throw new InputParameterIsNotCorrect(err.ErrorMessage);
                }
            }
        }
    }
}