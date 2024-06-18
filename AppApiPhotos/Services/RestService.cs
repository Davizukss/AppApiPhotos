using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AppApiPhotos.Models;

internal class RestService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _serializerOptions;

    public RestService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        List<Post> posts = new List<Post>();
        Uri uri = new Uri("https://jsonplaceholder.typicode.com/photos");

        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                posts = JsonSerializer.Deserialize<List<Post>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching data: {ex.Message}");
        }

        return posts;
    }
}
