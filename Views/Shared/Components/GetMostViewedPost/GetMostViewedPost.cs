using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

public class GetMostViewedPostViewComponent : ViewComponent
{
    private readonly IPostRepository _postRepository;

    public GetMostViewedPostViewComponent(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public IViewComponentResult Invoke()
    {
        ViewBag.myStringTitle = "MOST VIEWED POSTS";
        return View("_ListPosts", _postRepository.GetMostViewedPost(1));
    }
}