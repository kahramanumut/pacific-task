using Moq;
using PacificTask.Application.Image.Query;
using PacificTask.Client;
using PacificTask.Data.Image;

namespace PacificTask.Tests.Application.Image.Query;

public class GetImageForUserQueryHandlerTest
{
    private readonly Mock<IImageRepository> _mockImageRepository;
    private readonly Mock<IImageApiClient> _mockImageApiClient;
    private readonly GetImageForUserQueryHandler _handler;

    public GetImageForUserQueryHandlerTest()
    {
        _mockImageRepository = new Mock<IImageRepository>();
        _mockImageApiClient = new Mock<IImageApiClient>();
        _handler = new GetImageForUserQueryHandler(_mockImageRepository.Object, _mockImageApiClient.Object);
    }

    [Theory]
    [InlineData("user6", "https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/6")]
    public async Task GetImageForUserAsync_LastDigit6789_ReturnsJsonUrl(string userIdentifier, string expectedUrl)
    {
        //Arrange
        var query = new GetImageForUserQuery(userIdentifier);
        _mockImageApiClient.Setup(x => x.GetImage("6", CancellationToken.None)).ReturnsAsync(new ImageModel(6, expectedUrl));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedUrl, result.Url);
    }

    [Theory]
    [InlineData("user1", 1, "https://example.com/image1.png")]
    public async Task GetImageForUserAsync_LastDigit12345_ReturnsDbUrl(string userIdentifier, int id, string expectedUrl)
    {
        // Arrange
        var query = new GetImageForUserQuery(userIdentifier);
        _mockImageRepository.Setup(repo => repo.GetImageByIdAsync(id)).ReturnsAsync(expectedUrl);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedUrl, result.Url);
        _mockImageRepository.Verify(repo => repo.GetImageByIdAsync(id), Times.Once);
    }

    [Theory]
    [InlineData("aeiou", "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150")]
    public async Task GetImageForUserAsync_ContainsVowel_ReturnsVowelUrl(string userIdentifier, string expectedUrl)
    {
        // Arrange
        var query = new GetImageForUserQuery(userIdentifier);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedUrl, result.Url);
    }

    [Theory]
    [InlineData("pcfc!", "https://api.dicebear.com/8.x/pixel-art/png?seed")]
    public async Task GetImageForUserAsync_ContainsNonAlphanumeric_ReturnsRandomSeedUrl(string userIdentifier, string expectedUrl)
    {
        // Arrange
        var query = new GetImageForUserQuery(userIdentifier);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Contains(expectedUrl, result.Url);
    }

    [Theory]
    [InlineData("c0", "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150")]
    public async Task GetImageForUserAsync_NoConditionsMet_ReturnsDefaultUrl(string userIdentifier, string expectedUrl)
    {
          // Arrange
        var query = new GetImageForUserQuery(userIdentifier);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedUrl, result.Url);
    }
}
