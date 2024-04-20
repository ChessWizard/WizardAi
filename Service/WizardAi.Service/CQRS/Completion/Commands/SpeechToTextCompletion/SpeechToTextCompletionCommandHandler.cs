using MediatR;
using OpenAI_API.Completions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Dto;
using WizardAi.Core.Enums;
using WizardAi.Core.Extensions;
using WizardAi.Core.External;
using WizardAi.Core.Helpers;
using WizardAi.Core.Patterns.Builder;
using WizardAi.Service.CQRS.Completion.Queries.FullTextCompletion;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Completion.Commands.SpeechToTextCompletion
{
    public class SpeechToTextCompletionCommandHandler : IRequestHandler<SpeechToTextCompletionCommand, Result<SpeechToTextCompletionCommandResult>>
    {
        private readonly IOpenAiService _openAiService;

        public SpeechToTextCompletionCommandHandler(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public async Task<Result<SpeechToTextCompletionCommandResult>> Handle(SpeechToTextCompletionCommand request, CancellationToken cancellationToken)
        {
            var audioText = (await _openAiService.GetSpeechToTextDetailsAsync(request.AudioArray)).text;

            if(string.IsNullOrWhiteSpace(audioText))
                return Result<SpeechToTextCompletionCommandResult>.Error("İsteğinize uygun bir cevap üretilemedi!", (int)HttpStatusCode.BadRequest);

            var prompt = new TextPromptBuilder()
                .SetSubject(audioText)
                .SetPromptType(TextPromptType.None)
                .Build();

            var completionRequest = CompletionHelper.GenerateCompletionRequestByCreativity(request.CreativityType, prompt);

            var completion = await _openAiService.CreateTextCompletionAsync(completionRequest);

            if (completion is null || !completion.Completions.Any())
                return Result<SpeechToTextCompletionCommandResult>.Error("İsteğinize uygun bir cevap üretilemedi!", (int)HttpStatusCode.BadRequest);

            var options = completion.Completions
                .Select(completion => new TextCompletionOptionDto
                {
                    Message = completion.Text
                                        .Replace("\n", ""),
                    WordCount = completion.Text
                                          .Replace("\n", "")
                                          .GetWordCount()
                }).ToList();

            SpeechToTextCompletionCommandResult result = new()
            {
                Options = options
            };

            return Result<SpeechToTextCompletionCommandResult>.Success(result, (int)HttpStatusCode.OK);
        }
    }
}
