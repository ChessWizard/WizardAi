using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardAi.Service.CQRS.Image.Commands.TextToImageGeneration
{
    public class TextToImageGenerationCommandResult
    {
        public List<string> Base64FormattedImages { get; set; }
    }
}
