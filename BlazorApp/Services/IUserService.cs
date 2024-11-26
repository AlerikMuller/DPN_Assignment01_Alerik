namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto requst);
    public Task UpdateUserAsync(int id, UpdateUserDto request);
    public Task<UserDto> DeleteUserAsync(DeleteUserDto requst);
    public Task GetSingleAsync(int id, GetUserDto requst);
}