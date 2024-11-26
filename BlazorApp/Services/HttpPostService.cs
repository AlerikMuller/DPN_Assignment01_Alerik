using System.Text.Json;

namespace BlazorApp.Services;

public class HttpPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }

    public Task<PostDto> AddPostAsync(CreatePostDto request)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("posts", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public Task UpdatePostAsync(int id, UpdatePostDto request)
    {
        return Task.CompletedTask;
    }

    public Task<PostDto> DeletePostAsync(DeletePostDto requst)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("posts", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        })!;
    }

    public Task GetSingleAsync(int id, GetPostDto requst)
    {
        return Task.CompletedTask;
    }
}