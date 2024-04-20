using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Dto;

namespace WizardAi.Service.CQRS.Completion.Queries.FullTextStreamingCompletion
{
    public class FullTextStreamingCompletionQueryResult
    {
        public List<TextCompletionOptionDto> Options { get; set; }
    }
}
