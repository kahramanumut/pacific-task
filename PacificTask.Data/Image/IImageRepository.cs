namespace PacificTask.Data.Image;

public interface IImageRepository
{
    public Task<string> GetImageByIdAsync(int id);
}