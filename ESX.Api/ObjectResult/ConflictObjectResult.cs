using Microsoft.AspNetCore.Http;

namespace ESX.Api.ObjectResult
{
    public class ConflictObjectResult : Microsoft.AspNetCore.Mvc.ObjectResult
    {
        public ConflictObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status409Conflict;
        }

        public ConflictObjectResult() : this(null)
        {
            StatusCode = StatusCodes.Status409Conflict;
        }
    }
}