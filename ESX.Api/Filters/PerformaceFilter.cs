using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESX.Api.Filters
{
    public class PerformaceFilter : IAsyncActionFilter
    {
        private readonly string _nomePropriedade = "TempoDeProcessamento";
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var Stop = new Stopwatch();

            Stop.Start();

            var resultContext = await next();

            Stop.Stop();

            if (resultContext.Result is Microsoft.AspNetCore.Mvc.ObjectResult view)
            {
                var item = view.Value;

                if (item.GetType().GetProperty(_nomePropriedade) != null)
                {
                    var total = Convert.ToInt64(Stop.Elapsed.TotalMilliseconds);
                    item.GetType().GetProperty(_nomePropriedade).SetValue(item, total);
                }
                view.Value = item;
            }
        }
    }
}