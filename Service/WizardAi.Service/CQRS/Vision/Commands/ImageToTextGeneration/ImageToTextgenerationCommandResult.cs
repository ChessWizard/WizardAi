using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Dto;

namespace WizardAi.Service.CQRS.Vision.Commands.ImageToTextGeneration
{
    public class ImageToTextgenerationCommandResult
    {
        public List<TextCompletionOptionDto> Options { get; set; }
    }
}
