using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filepath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filepath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson);
        int maxId = posts.Count > 0 ? posts.Max(p => p.Id) : 1;
        post.Id = maxId + 1;
        posts.Add(post);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filepath, postsAsJson);
        return post;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllText(filepath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson);
        return posts.AsQueryable();
    }
    
    public async Task<Post> DeleteAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filepath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson);
        int maxId = posts.Count > 0 ? posts.Max(p => p.Id) : 1;
        post.Id = maxId - 1;
        posts.Delete(post);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filepath, postsAsJson);
        return post;
    }
    
    public async Task<Post> OverwriteAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filepath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filepath, postsAsJson);
        return post;
    }
}