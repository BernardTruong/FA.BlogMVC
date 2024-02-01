using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlogMVC.Controllers.Tag
{
    public class TagController : Controller
    {
        private readonly IPostRepository _postRepository;

        public TagController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public IActionResult GetTagByCate(string tag)
        {
            ViewBag.bigTitle = tag;
            ViewBag.image = "/assets/img/home-bg.jpg";
            ViewBag.tag = tag;
            return View(_postRepository.GetPostsByTag(tag));
        }
    }
}
