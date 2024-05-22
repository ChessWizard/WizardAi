using MediatR;
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
using WizardAi.Service.CQRS.Completion.Queries.FullTextCompletion;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Completion.Queries.FullTextStreamingCompletion
{
    public class FullTextStreamingCompletionQueryHandler : IRequestHandler<FullTextStreamingCompletionQuery, Result<Unit>>
    {
        private readonly IOpenAiService _openAiService;

        public FullTextStreamingCompletionQueryHandler(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public async Task<Result<Unit>> Handle(FullTextStreamingCompletionQuery request, CancellationToken cancellationToken)
        {
            var prompt = new TextPromptBuilder()
                .SetSubject(request.Subject)
                .SetDescription(request.Description)
                .SetPromptType(request.PromptType)
                .SetWordCount(request.WordCount)
                .SetLanguageType(request.LanguageType)
                .Build();

            var completionRequest = CompletionHelper.GenerateCompletionRequestByCreativity(request.CreativityType, prompt);

            await _openAiService.CreateStreamingCompletionAsync(completionRequest, request.ConnectionId);

            return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
        }
    }
}
