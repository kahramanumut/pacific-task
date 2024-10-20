using PacificTask.Data.Image;
using Microsoft.Data.Sqlite;

namespace PacificTask.Tests.Data;

public class ImageRepositoryTests
{
    private readonly SqliteConnection _connection;

    public ImageRepositoryTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        CreateTableAndInsertData();
    }

    private void CreateTableAndInsertData()
    {
        _connection.Open();
        var createTableCmd = _connection.CreateCommand();
        createTableCmd.CommandText =
        @"
            CREATE TABLE images (
                id INTEGER PRIMARY KEY,
                url TEXT
            );
            INSERT INTO images (id, url) VALUES (1, 'https://example.com/image1.png');
            INSERT INTO images (id, url) VALUES (2, 'https://example.com/image2.png');
        ";
        createTableCmd.ExecuteNonQuery();
    }

    [Fact]
    public async Task GetImageByIdAsync_ValidId_ReturnsImageUrl()
    {
        //Arrange
        var repository = new ImageRepository(_connection);

        //Act
        var result = await repository.GetImageByIdAsync(1);

        //Assert
        Assert.Equal("https://example.com/image1.png", result);
    }

    [Fact]
    public async Task GetImageByIdAsync_InvalidId_ThrowsException()
    {
        //Arrange
        var repository = new ImageRepository(_connection);

        //Act
        var result = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await repository.GetImageByIdAsync(999);
        });

        //Assert
        Assert.Equal(typeof(InvalidOperationException), result.GetType());
        Assert.Equal("Image not found", result.Message);
    }
}