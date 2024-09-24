using System.Text.Json;
using Entities;
using RepositoryContracts;

public class UserFileRepository : IUserRepository
{
    private readonly string filepath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
        return user;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllText(filepath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        return users.AsQueryable();
    }
    
    public async Task<User> DeleteAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 1;
        user.Id = maxId - 1;
        users.Delete(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
        return user;
    }
    
    public async Task<User> OverwriteAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
        return user;
    }
}