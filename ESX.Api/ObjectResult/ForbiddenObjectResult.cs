using Microsoft.AspNetCore.Http;

namespace ESX.Api.ObjectResult
{
    public class ForbiddenObjectResult : Microsoft.AspNetCore.Mvc.ObjectResult
    {
        public ForbiddenObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }

        public ForbiddenObjectResult() : this(null)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}