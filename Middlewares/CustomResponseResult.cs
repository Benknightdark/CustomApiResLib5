using CutomApiLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CustomWebApiLib.Models;

namespace CutomApiLib.Middlewares
{
    public class CustomResponseResult : Attribute, IActionFilter
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
