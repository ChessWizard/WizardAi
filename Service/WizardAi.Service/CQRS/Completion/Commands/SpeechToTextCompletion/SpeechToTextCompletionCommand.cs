using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Completion.Commands.SpeechToTextCompletion
{
    public class SpeechToTextCompletionCommand : IRequest<Result<SpeechToTextCompletionCommandResult>>
    {
        public byte[] AudioArray { get; set; }

        public CreativityType CreativityType { get; set; }
    }
}
