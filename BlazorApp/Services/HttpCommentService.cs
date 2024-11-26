using System.Text.Json;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }

    public Task<CommentDto> AddCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public Task UpdateCommentAsync(int id, UpdateCommentDto request)
    {
        return Task.CompletedTask;
    }

    public Task<CommentDto> DeleteCommentAsync(DeleteCommentDto requst)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        })!;
    }

    public Task GetSingleAsync(int id, GetCommentDto requst)
    {
        return Task.CompletedTask;
    }
}