using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using FA.JustBlog.Core.Models;

namespace FA.JustBlogMVC.Views.HtmlCustomHelper
{
    public static class DateHelper
    {

        public static IHtmlContent ToFriendlyFormat(this IHtmlHelper htmlHelper, Post post)
        {
            string status = string.Empty;

            TimeSpan timeSincePosted = DateTime.Now - post.PostedOn;

            if (timeSincePosted.TotalMinutes < 1)
            {
                status = $"Just now with rate {post.TotalRate} by {post.ViewCount} view(s) ";
            }
            else if (timeSincePosted.TotalMinutes < 60)
            {
                status = $"{(int)timeSincePosted.TotalMinutes} minutes ago with rate {post.TotalRate} by {post.ViewCount} view(s)";
            }
            else if (timeSincePosted.TotalHours < 24)
            {
                status = $"{(int)timeSincePosted.TotalHours} hours ago with rate {post.TotalRate} by {post.ViewCount} view(s)";
            }
            else if (timeSincePosted.TotalDays == 1)
            {
                status = $"Yesterday at " + post.PostedOn.ToString("h:mm tt") + $"with rate {post.TotalRate} by {post.ViewCount} view(s)";
            }
            else
            {
                status = post.PostedOn.ToString("MMMM d 'at' h:mm tt") + $" with rate {post.TotalRate} by {post.ViewCount} view(s)";
            }
            return new HtmlString("<p class=\"text-muted italic-text \" style = \"margin = 0 0\">" + status + "</p>");
        }
    }
}
