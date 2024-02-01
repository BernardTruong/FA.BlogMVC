using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FA.JustBlogMVC.Views.HtmlCustomHelper
{
    public static class CategoryLink
    {
        public static IHtmlContent CategoryLinkHelper(this IHtmlHelper htmlHelper, string CategoryName, string textColor)
        {
            var urlHelperFactory = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var actionContext = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IActionContextAccessor>().ActionContext;
            var urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
            var url = urlHelper.Action("GetPostByCate", "Post", new { Category = CategoryName });

            return new HtmlString($"<a class=\"nav-link {textColor}\" href = {url}>{CategoryName} </a>");
        }
    }
}
