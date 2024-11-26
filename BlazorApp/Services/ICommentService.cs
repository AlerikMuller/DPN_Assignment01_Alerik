namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CommentDto> AddCommentAsync(CreateCommentDto requst);
    public Task UpdateCommentAsync(int id, UpdateCommentDto request);
    public Task<CommentDto> DeleteCommentAsync(DeleteCommentDto requst);
    public Task GetSingleAsync(int id, GetCommentDto requst);
}