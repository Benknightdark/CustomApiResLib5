using CutomApiLib.Models;
using CustomWebApiLib.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CutomApiLib.Middlewares
{
    public class CustomResponseResult : IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
             var ignore = context.ActionDescriptor.FilterDescriptors
                .Select (f => f.Filter)
                .OfType<ServiceFilterAttribute> ()
                .Any (f => f.ServiceType.Equals (typeof (IgonreApiAuthorize)));
            if(ignore) return;

            if (context.Result is ObjectResult objectResult)
            {

                if (objectResult.StatusCode != 200)
                {
                    objectResult.Value = new CustomResponseModel
                    {
                        status = (int)objectResult.StatusCode,
                        title = objectResult.StatusCode.ToString(),
                        errors = objectResult.Value
                    };
                }
                else
                {
                    objectResult.Value = new CustomResponseModel
                    {
                        status = (int)objectResult.StatusCode,
                        data = objectResult.Value
                    };
                }

            }
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            
        }


    }
}
