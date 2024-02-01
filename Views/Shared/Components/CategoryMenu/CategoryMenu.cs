using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
public class CategoryMenuViewComponent : ViewComponent
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryMenuViewComponent(ICategoryRepository CategoryRepository)
    {
        _categoryRepository = CategoryRepository;
    }
    public IViewComponentResult Invoke()
    {
        ViewBag.myStringTitle = "LATEST POSTS";
        return View("CategoryMenu", _categoryRepository.GetAllCategories());
    }

}
