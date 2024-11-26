namespace BlazorApp.Services;

public class IPostService
{
    public Task<PostDto> AddPostAsync(CreatePostDto requst);
    public Task UpdatePostAsync(int id, UpdatePostDto request);
    public Task<PostDto> DeletePostAsync(DeletePostDto requst);
    public Task GetSingleAsync(int id, GetPostDto requst);
}