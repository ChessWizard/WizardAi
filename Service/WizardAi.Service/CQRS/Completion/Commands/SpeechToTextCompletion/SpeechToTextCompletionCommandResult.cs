﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Dto;

namespace WizardAi.Service.CQRS.Completion.Commands.SpeechToTextCompletion
{
    public class SpeechToTextCompletionCommandResult
    {
        public List<TextCompletionOptionDto> Options { get; set; }
    }
}
