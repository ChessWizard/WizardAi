using MediatR;
using Microsoft.AspNetCore.Mvc;
using WizardAi.Service.CQRS.Completion.Commands.SpeechToTextCompletion;
using WizardAi.Service.CQRS.Completion.Queries.FullTextCompletion;
using WizardAi.Service.CQRS.Image.Commands.TextToImageGeneration;

namespace WizardAi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGenerationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageGenerationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateImageFromText([FromBody] TextToImageGenerationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
