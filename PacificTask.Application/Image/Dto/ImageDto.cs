using System.Text.Json.Serialization;

namespace PacificTask.Application.Image.Dto;

public record ImageDto([property: JsonPropertyName("url")] string Url);