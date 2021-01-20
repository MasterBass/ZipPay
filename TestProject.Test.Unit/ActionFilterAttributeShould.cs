using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using TestProject.API.Filters;
using TestProject.Common.Exception;
using Xunit;

namespace TestProject.Test.Unit
{
    public class ActionFilterAttributeShould
    {
        [Fact]
        public void ValidateModelAttributeShould()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("", "error");
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext(
                    httpContext,
                    new RouteData(),
                    new ActionDescriptor(),
                    modelState
                ),
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            var sut = new ValidateModelAttribute();
            
            Assert.Throws<InputParameterIsNotCorrect>(() => sut.OnActionExecuting(context));
        }
    }
}