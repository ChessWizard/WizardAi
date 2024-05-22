using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardAi.Core.Enums;
using WizardAi.Service.CQRS.Vision.Commands.ImageToTextGeneration;

namespace WizardAi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VisionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ImageToTextGeneration([FromForm]ImageToTextGenerationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
