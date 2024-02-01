using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FA.JustBlogMVC.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;

        public HomeController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IActionResult Index()
        {
            ViewBag.image = "/assets/img/home-bg.jpg";
            ViewBag.myStringTitle = "ALL POSTS";
            ViewBag.bigTitle = "Home";
            return View(_postRepository.GetAllPosts());
        }
        public IActionResult AboutCard()
        {
            ViewBag.bigTitle = "About me";
            ViewBag.image = "/assets/img/post-bg.jpg";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}