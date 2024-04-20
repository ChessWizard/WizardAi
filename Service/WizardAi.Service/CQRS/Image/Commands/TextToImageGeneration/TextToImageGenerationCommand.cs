using MediatR;
using OpenAI_API.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Image.Commands.TextToImageGeneration
{
    public class TextToImageGenerationCommand : IRequest<Result<TextToImageGenerationCommandResult>>
    {
        public string Content { get; set; }

        public int RequestedOption { get; set; } = 1;

        public ImageType ImageType { get; set; }

        public ImageSizeType ImageSizeType { get; set; } = ImageSizeType.Normal;

        public ImageQualityType ImageQualityType { get; set; } = ImageQualityType.Standard;
    }
}
