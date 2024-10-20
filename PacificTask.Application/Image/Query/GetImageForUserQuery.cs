using MediatR;
using PacificTask.Application.Image.Dto;
using PacificTask.Client;
using PacificTask.Data.Image;

namespace PacificTask.Application.Image.Query;

public class GetImageForUserQuery : IRequest<ImageDto>
{
    public GetImageForUserQuery(string userIdentifier)
    {
        UserIdentifier = userIdentifier;
    }
    
    public string UserIdentifier { get; }

    public string LastChar => UserIdentifier[^1].ToString();
}

public class GetImageForUserQueryHandler : IRequestHandler<GetImageForUserQuery, ImageDto>
{
    private readonly IImageRepository _imageRepository;
    private readonly IImageApiClient _imageClient;

    public GetImageForUserQueryHandler(IImageRepository imageRepository, IImageApiClient imageClient)
    {
        _imageRepository = imageRepository;
        _imageClient = imageClient;
    }
    public async Task<ImageDto> Handle(GetImageForUserQuery request, CancellationToken cancellationToken)
    {
        var vowels = "aeiou";

        //if the last character of the user identifier is [6, 7, 8, 9]
        if ("6789".Contains(request.LastChar))
        {
            var imageUrl = (await _imageClient.GetImage(request.LastChar, cancellationToken)).Url;
            return new ImageDto(imageUrl);
        }

        //If the user last character of the user identifier is [1, 2, 3, 4, 5]
        if ("12345".Contains(request.LastChar))
        {
            var imageUrlFromDb = await _imageRepository.GetImageByIdAsync(int.Parse(request.LastChar));

            return new ImageDto(imageUrlFromDb);
        }

        //If the user identifier contains at least one vowel character (aeiou)
        if (request.UserIdentifier.Any(c => vowels.Contains(c)))
        {
            return new ImageDto("https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150");
        }

        //If the user identifier contains a non-alphanumeric character
        if (request.UserIdentifier.Any(c => !char.IsLetterOrDigit(c)))
        {
            var random = new Random().Next(1, 6);
            return new ImageDto($"https://api.dicebear.com/8.x/pixel-art/png?seed={random}&size=150");
        }

        //If none of the above conditions are met
        return new ImageDto("https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150");
    }
}