using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Libary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Libary.Extensions
{
    public static class CustomValidationResponseExtension
    {

        public static void UseCustomValidationResponse(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count() > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    ErrorDto errorDto = new ErrorDto(errors.ToList(), true);
                    var response = Response<NoContentDto>.Fail(errorDto, StatusCodes.Status400BadRequest);

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
