using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CasheServices;
using System.Text;

namespace Store.Web.Helper
{
    public class CasheAttribute:Attribute,IAsyncActionFilter
    {
        private readonly int _timeToLiveInSec;

        public CasheAttribute(int timeToLiveInSec)
        {
            _timeToLiveInSec = timeToLiveInSec;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var _casheService = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();
            var casheKey = GenerateCasheKeyFromRequest(context.HttpContext.Request);
            var CasheResponse = await _casheService.GetCasheResponseAsync(casheKey);
            if (!string.IsNullOrEmpty(CasheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = CasheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = contentResult;
                return;
            } 
            var executeResponse =await next();

            if(executeResponse.Result is OkObjectResult response)
            {
                await _casheService.SetCasheResponseAsync(casheKey,response.Value,TimeSpan.FromSeconds(_timeToLiveInSec));
            }

        }
        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            StringBuilder CasheKey = new StringBuilder();

            CasheKey.Append($"{request.Path}");

            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
                 CasheKey.Append($"|{key}-{value}");

            return CasheKey.ToString();
        }
    }
}
