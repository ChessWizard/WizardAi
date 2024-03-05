using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Completion.Queries.FullTextCompletion
{
    public class FullTextCompletionQuery : IRequest<Result<FullTextCompletionQueryResult>>
    {
        public string Subject { get; set; }

        public string? Description { get; set; }

        public int? WordCount { get; set; }

        public PromptType PromptType { get; set; }

        public CreativityType CreativityType { get; set; }

        public int RequestedOption { get; set; } = 3;
    }
}
