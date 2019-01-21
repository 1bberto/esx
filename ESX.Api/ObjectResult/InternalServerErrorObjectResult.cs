using Microsoft.AspNetCore.Http;

namespace ESX.Api.ObjectResult
{
    public class InternalServerErrorObjectResult : Microsoft.AspNetCore.Mvc.ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorObjectResult() : this(null)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}