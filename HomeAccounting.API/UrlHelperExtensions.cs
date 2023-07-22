using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.API
{
    public static class UrlHelperExtensions
    {
        public static IUrlHelper GetUrlHelper(this IUrlHelperFactory urlHelperFactory, ActionContext actionContext)
        {
            return urlHelperFactory.GetUrlHelper(actionContext);
        }
    }
}
