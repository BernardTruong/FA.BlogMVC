using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.Extensions.Hosting;
using FA.JustBlogMVC.Areas.Admin.Models;
using Azure;
using Microsoft.AspNetCore.Authorization;
using FA.JustBlogMVC.Utility;

namespace FA.JustBlogMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostsController(IPostRepository postRepository, ITagRepository tagRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
        }

        //Home page, show all posts with crud [self-gen code]
        [Authorize(Roles = SD.Role_User + "," + SD.Role_BOwner + "," + SD.Role_Contributor)]
        public IActionResult Index()
        {
            return View(_postRepository.GetAllPosts());
        }
        public IActionResult allPost()
        {
            return View(_postRepository.GetAllPosts());
        }

        public IActionResult latestPost()
        {
            return View("allPost", _postRepository.GetLatestPost(1));
        }

        public IActionResult mostViewedPost() 
        {
            return View("allPost", _postRepository.GetMostViewedPost(1));
        }

        public IActionResult postedPost()
        {
            return View("allPost", _postRepository.GetPublisedPosts());
        }

        public IActionResult unPostedPost()
        {
            return View("allPost", _postRepository.GetUnpublisedPosts());
        }

        // GET: Admin/Post/Create
        [Authorize(Roles = SD.Role_BOwner + "," + SD.Role_Contributor)]
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
            ViewData["Tag"] = new SelectList(_tagRepository.GetAllTags(), "TagId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_BOwner + "," + SD.Role_Contributor)]
        public async Task<IActionResult> Create(PostTagViewModel postTagVM)
        {
            //check model valid to submit data to DB
            if (ModelState.IsValid)
            {
                List<PostTag> _postTags = new List<PostTag>();
                foreach (var item in postTagVM.Tag)
                {
                    _postTags.Add(new PostTag() { PostId = postTagVM.Post.Id, TagId = item });
                }
                postTagVM.Post.PostTag = _postTags;
                _postRepository.AddPost(postTagVM.Post);
                return RedirectToAction(nameof(Index));
            }
            //if its invalid, show fault on screen accourding to data annotation
            else {
                ViewData["Category"] = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
                ViewData["Tag"] = new SelectList(_tagRepository.GetAllTags(), "TagId", "Name");
                return View(); }
        }


        // GET: Admin/Post/Delete/5
        [Authorize(Roles = SD.Role_BOwner)]
        public async Task<IActionResult> Delete(int id)
        {
            _postRepository.DeletePostById(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_BOwner + "," + SD.Role_Contributor)]
        public async Task<IActionResult> Edit(int id, PostTagViewModel postTagVM)
        {
            List<PostTag> _postTags = new List<PostTag>();

            //because posttag is a list, so i create this list
            foreach (var item in postTagVM.Tag)
            {
                _postTags.Add(new PostTag() { PostId = postTagVM.Post.Id, TagId = item });
            }
            postTagVM.Post.PostTag = _postTags;

            _postRepository.UpdatePost(postTagVM.Post);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles =SD.Role_BOwner + "," + SD.Role_Contributor)]
        public async Task<IActionResult> Edit(int id)
        {
            //Repo
            //create ViewModel
            ViewData["Category"] = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
            ViewData["Tag"] = new SelectList(_tagRepository.GetAllTags(), "TagId", "Name");
            Post post = _postRepository.FindPost(id);
            List<int> tag = new List<int>();
            foreach (var item in post.PostTag)
            {
                if (item.PostId == id)
                {
                    tag.Add(item.TagId);
                }
            }
            PostTagViewModel postTagVM = new PostTagViewModel() {Post = post, Tag = tag};
            return View(postTagVM);
        }

        // GET: Admin/Post/Details/5
        [Authorize(Roles = SD.Role_User + "," + SD.Role_BOwner + "," + SD.Role_Contributor)]
        public async Task<IActionResult> Details(int id)
        {

            return View(_postRepository.FindPost(id));
        }
    }
}
