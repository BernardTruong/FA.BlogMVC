using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using FA.JustBlog.Core.Models;
using System;
using FA.JustBlog.Core.Repositories;

namespace FA.JustBlogMVC.Controllers.Post
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
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

        public IActionResult Detail(int Id)
        {
            return View(_postRepository.FindPost(Id));
        }

        public IActionResult GetPostByCate(string Category)
        {
            ViewBag.bigTitle = Category;
            ViewBag.image = "/assets/img/home-bg.jpg";
            return View("Index",_postRepository.GetPostsByCategory(Category));
        }

        public ActionResult Details(int year, int month, int id)
        {
            var post = _postRepository.FindPost(year, month, id);
            ViewBag.bigTitle = post.Title;
            ViewBag.image = "/assets/img/home-bg.jpg";
            return View(post);
        }

    }
}
