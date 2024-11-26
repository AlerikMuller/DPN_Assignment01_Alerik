using EfcRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext _context;

    public EfcPostRepository(AppContext context)
    {
        _context = context;
    }

    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> entityEntry = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await _context.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new Exception($"Post with ID {post.Id} not found");
        }

        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existingPost = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existingPost == null)
        {
            throw new Exception($"Post with ID {id} not found");
        }

        _context.Posts.Remove(existingPost);
        await _context.SaveChangesAsync();
    }

    public async Task<Post?> GetSingleAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Post> GetMany()
    {
        return _context.Posts.AsQueryable();
    }
}