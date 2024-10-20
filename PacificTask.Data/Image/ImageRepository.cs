using Microsoft.Data.Sqlite;
using Dapper;

namespace PacificTask.Data.Image;

public class ImageRepository : IImageRepository
{
     private readonly SqliteConnection _connection;
    private const string GetImageByIdQuery = "SELECT url FROM images WHERE id = @Id";

    public ImageRepository(SqliteConnection connection)
    {
        _connection = connection;
    }

    public async Task<string> GetImageByIdAsync(int id)
    {
        await _connection.OpenAsync();
        var imageUrl = await _connection.QuerySingleOrDefaultAsync<string>(GetImageByIdQuery, new { Id = id });
        await _connection.CloseAsync();
        return imageUrl ?? throw new InvalidOperationException("Image not found");
    }
}