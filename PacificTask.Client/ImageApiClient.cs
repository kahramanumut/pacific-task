using System.Text.Json;

namespace PacificTask.Client;

public class ImageApiClient : IImageApiClient
{
    private readonly HttpClient _httpClient;

    public ImageApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ImageModel> GetImage(string imageId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"ck-pacificdev/tech-test/images/{imageId}", cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var image = JsonSerializer.Deserialize<ImageModel>(content);
            return image ?? throw new HttpRequestException("Image URL could not be retrieved from API");
        }

        throw new HttpRequestException("Image could not be retrieved from external API");
    }
}