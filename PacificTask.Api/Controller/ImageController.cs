using MediatR;
using Microsoft.AspNetCore.Mvc;
using PacificTask.Application.Image.Query;

namespace PacificTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userIdentifier}")]
        public async Task<IActionResult> GetImage(string userIdentifier)
        {
            var imageUrl = await _mediator.Send(new GetImageForUserQuery(userIdentifier));
            return Ok(imageUrl);
        }
    }
}
