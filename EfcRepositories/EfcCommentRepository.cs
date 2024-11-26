using EfcRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext _context;

    public EfcCommentRepository(AppContext context)
    {
        _context = context;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!(await _context.Comments.AnyAsync(p => p.Id == comment.Id)))
        {
            throw new Exception($"Comment with ID {comment.Id} not found");
        }

        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existingComment = await _context.Comments.SingleOrDefaultAsync(p => p.Id == id);
        if (existingComment == null)
        {
            throw new Exception($"Comment with ID {id} not found");
        }

        _context.Comments.Remove(existingComment);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment?> GetSingleAsync(int id)
    {
        return await _context.Comments
            .Include(p => p.User)
            .Include(p => p.Comments)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Comment> GetMany()
    {
        return _context.Comments.AsQueryable();
    }
}