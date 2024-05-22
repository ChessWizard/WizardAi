using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardAi.Core.External;
using WizardAi.Service.CQRS.Completion.Commands.SpeechToTextCompletion;
using WizardAi.Service.CQRS.Completion.Queries.FullTextCompletion;
using WizardAi.Service.CQRS.Completion.Queries.FullTextStreamingCompletion;

namespace WizardAi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompletionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("fullText")]
        public async Task<IActionResult> GetFullTextCompletions([FromQuery] FullTextCompletionQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("fullText-streaming")]
        public async Task<IActionResult> GetFullTextStreamingCompletions([FromQuery] FullTextStreamingCompletionQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("speech")]
        public async Task<IActionResult> CreateSpeechToTextCompletions([FromBody] SpeechToTextCompletionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
