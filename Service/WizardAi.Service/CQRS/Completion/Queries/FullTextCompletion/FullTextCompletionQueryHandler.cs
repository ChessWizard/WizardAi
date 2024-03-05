using MediatR;
using OpenAI_API.Completions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Dto;
using WizardAi.Core.Extensions;
using WizardAi.Core.External;
using WizardAi.Core.Helpers;
using WizardAi.Core.Patterns.Builder;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Completion.Queries.FullTextCompletion
{
    public class FullTextCompletionQueryHandler : IRequestHandler<FullTextCompletionQuery, Result<FullTextCompletionQueryResult>>
    {
        private readonly IOpenAiService _openAiService;

        public FullTextCompletionQueryHandler(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public async Task<Result<FullTextCompletionQueryResult>> Handle(FullTextCompletionQuery request, CancellationToken cancellationToken)
        {
            var prompt = new PromptBuilder()
                .SetSubject(request.Subject)
                .SetDescription(request.Description)
                .SetPromptType(request.PromptType)
                .SetWordCount(request.WordCount)
                .Build();

            var completionRequest = CompletionHelper.GenerateCompletionRequestByCreativity(request.CreativityType, prompt);

            var completion = await _openAiService.CreateTextCompletionsAsync(completionRequest, request.RequestedOption);

            if (completion is null || !completion.Completions.Any())
                return Result<FullTextCompletionQueryResult>.Error("İsteğinize uygun bir cevap üretilemedi!", (int)HttpStatusCode.BadRequest);

            var options = completion.Completions
                .Select(completion => new TextCompletionOptionDto
                {
                    Message = completion.Text.Replace("\n", ""),
                    WordCount = completion.Text.Replace("\n", "").GetWordCount()
                }).ToList();

            FullTextCompletionQueryResult result = new()
            {
                Options = options,
            };

            return Result<FullTextCompletionQueryResult>.Success(result, (int)HttpStatusCode.OK);
        }
    }
}
