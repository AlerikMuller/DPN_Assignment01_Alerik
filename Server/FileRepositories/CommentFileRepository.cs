using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filepath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filepath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filepath, commentsAsJson);
        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllText(filepath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        return comments.AsQueryable();
    }
    
    public async Task<Comment> DeleteAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filepath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId - 1;
        comments.Delete(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filepath, commentsAsJson);
        return comment;
    }
    
    public async Task<Comment> OverwriteAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filepath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filepath, commentsAsJson);
        return comment;
    }
}