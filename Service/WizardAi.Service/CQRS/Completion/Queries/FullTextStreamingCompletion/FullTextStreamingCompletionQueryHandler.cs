using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Completion.Queries.FullTextStreamingCompletion
{
    public class FullTextStreamingCompletionQueryHandler : IRequestHandler<FullTextStreamingCompletionQuery, Result<FullTextStreamingCompletionQueryResult>>
    {
        public Task<Result<FullTextStreamingCompletionQueryResult>> Handle(FullTextStreamingCompletionQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
