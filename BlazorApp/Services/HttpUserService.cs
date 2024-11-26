using System.Text.Json;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }

    public Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public Task UpdateUserAsync(int id, UpdateUserDto request)
    {
        return Task.CompletedTask;
    }

    public Task<UserDto> DeleteUserAsync(DeleteUserDto requst)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        })!;
    }

    public Task GetSingleAsync(int id, GetUserDto requst)
    {
        return Task.CompletedTask;
    }
}