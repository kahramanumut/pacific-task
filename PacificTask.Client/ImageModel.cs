using System.Text.Json.Serialization;

namespace PacificTask.Client;

public class ImageModel
{
    [JsonConstructor]
    public ImageModel(int id, string url)
    {
        Id = id;
        Url = url;
    }

    [JsonPropertyName("id")]
    public int Id { get; private set; }

    [JsonPropertyName("url")]
    public string Url { get; private set; }
}