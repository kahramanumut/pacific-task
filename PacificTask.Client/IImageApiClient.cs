namespace PacificTask.Client;

public interface IImageApiClient
{
    Task<ImageModel> GetImage(string imageId, CancellationToken cancellationToken = default);
}