using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class CommentsController : ControllerBase
{
    private readonly ICommentsRpository commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }

    [HttpGet]
    public async Task<ActionResult<CommentDto>> AddComment([FromBody] CreateCommentDto request)
    {
        await VerifyCommentNameIsAvailableAsync(request.CommentName);
        Comment created = await commentRepo.AddAsync(comment);
        CommentDto dto = new()
        {
            Id = created.Id,
            CommentName = created.CommentName
        }
        return Created($"/Comments/{dto.Id}", created);
    }
    
    [HttpGet]
    public async Task<ActionResult<Comment>> AddComment([FromBody] CreateCommentDto request)
    {
        try
        {
            await VerifyCommentNameIsAvailableAsync(request.CommentName);
            Comment comment = new(request.CommentName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    public class CreateCommentDto
    {
        public required string CommentName { get; set; }
    }
}