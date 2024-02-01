using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FA.JustBlogMVC.Views.HtmlCustomHelper
{
    public static class TagLink
    {
            //var divBuilder = new TagBuilder("div");
            //divBuilder.AddCssClass("col-4");
            //divBuilder.MergeAttribute("style", "padding: 0 20px");

            //var pBuilder = new TagBuilder("p");
            //pBuilder.AddCssClass("border rounded text-center bg-primary text-light m-3");
            //pBuilder.InnerHtml.AppendHtml(tagName);

            //divBuilder.InnerHtml.AppendHtml(pBuilder);
            public static IHtmlContent TagLinkHelper(this IHtmlHelper htmlHelper, string tagName)
            {
                var urlHelperFactory = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
                var actionContext = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IActionContextAccessor>().ActionContext;
                var urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
                var url = urlHelper.Action("GetTagByCate", "Tag", new { tag = tagName });

                return new HtmlString($"<div style=\"padding: 0px 5px\">\r\n <a class=\"border rounded text-center bg-primary text-light p-2\" href = {url}>{tagName}</a>\r\n</div>");
            }

        }
    }

