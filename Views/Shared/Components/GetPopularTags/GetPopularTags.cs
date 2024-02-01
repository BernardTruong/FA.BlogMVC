using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

public class GetPopularTagsViewComponent : ViewComponent
{
    private readonly ITagRepository _tagRepository;

    public GetPopularTagsViewComponent(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public IViewComponentResult Invoke()
    {
        ViewBag.myStringTitle = "MOST POPULAR TAGS";
        return View("_PopularTags", _tagRepository.GetPopularTag(1));
    }
}