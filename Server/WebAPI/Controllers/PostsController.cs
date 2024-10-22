using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class PostsController : ControllerBase
{
    private readonly IPostsRpository postRepo;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    [HttpGet]
    public async Task<ActionResult<PostDto>> AddPost([FromBody] CreatePostDto request)
    {
        await VerifyPostNameIsAvailableAsync(request.PostName);
        Post created = await postRepo.AddAsync(post);
        PostDto dto = new()
        {
            Id = created.Id,
            PostName = created.PostName
        }
        return Created($"/Posts/{dto.Id}", created);
    }
    
    [HttpGet]
    public async Task<ActionResult<Post>> AddPost([FromBody] CreatePostDto request)
    {
        try
        {
            await VerifyPostNameIsAvailableAsync(request.PostName);
            Post post = new(request.PostName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    public class CreatePostDto
    {
        public required string PostName { get; set; }
    }
}