using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        public TagController(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View("Index", _tagRepository.GetAllTags().ToList());
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagRepository.Find((int)id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [Authorize(Roles = "Contributor,BlogOwner")]
        public IActionResult Create()
        {
            return View(new Tag());
        }

        [Authorize(Roles = "Contributor,BlogOwner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _tagRepository.AddTag(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [Authorize(Roles = "Contributor,BlogOwner")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagRepository.Find((int)id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [Authorize(Roles = "Contributor,BlogOwner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tagRepository.UpdateTag(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_tagRepository.Find(tag.TagId) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [Authorize(Roles = "BlogOwner")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagRepository.Find((int)id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [Authorize(Roles = "BlogOwner")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tag = _tagRepository.Find(id);
            if (tag != null)
            {
                _tagRepository.DeleteTag(tag);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
