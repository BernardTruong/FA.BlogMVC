using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

public class GetLatestPostViewComponent : ViewComponent
{
    private readonly IPostRepository _postRepository;

    public GetLatestPostViewComponent(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public IViewComponentResult Invoke()
    {
        ViewBag.myStringTitle = "LATEST POSTS";
        return View("_ListPosts", _postRepository.GetLatestPost(1));
    }
}