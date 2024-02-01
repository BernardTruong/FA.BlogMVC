using FA.JustBlog.Core.Models;

namespace FA.JustBlogMVC.Areas.Admin.Models
{
    public class PostTagViewModel
    {
        public Post Post { get; set; }
        public IList<int> Tag { get; set; }
    }
}
