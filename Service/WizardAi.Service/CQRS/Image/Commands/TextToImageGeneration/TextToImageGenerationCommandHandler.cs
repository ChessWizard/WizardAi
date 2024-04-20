using MediatR;
using OpenAI_API.Images;
using System.Net;
using WizardAi.Core.External;
using WizardAi.Core.Helpers;
using WizardAi.Core.Patterns.Builder;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Image.Commands.TextToImageGeneration
{
    public class TextToImageGenerationCommandHandler : IRequestHandler<TextToImageGenerationCommand, Result<TextToImageGenerationCommandResult>>
    {
        private readonly IOpenAiService _openAiService;

        public TextToImageGenerationCommandHandler(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public async Task<Result<TextToImageGenerationCommandResult>> Handle(TextToImageGenerationCommand request, CancellationToken cancellationToken)
        {
            var prompt = new ImagePromptBuilder()
                .SetDescription(request.Content)
                .SetImageType(request.ImageType)
                .Build();

            ImageGenerationRequest imageGenerationRequest = new()
            {
                Prompt = prompt,
                Model = OpenAI_API.Models.Model.DALLE3,
                NumOfImages = request.RequestedOption,
                Quality = request.ImageQualityType.ToString().ToLower(),
                ResponseFormat = ImageResponseFormat.B64_json,
                Size = ImageGenerationHelper.GetImageSizeByType(request.ImageSizeType)
            };

            var result = await _openAiService.GenerateImageFromTextAsync(imageGenerationRequest);

            if (result.Data is null || !result.Data.Any())
                return Result<TextToImageGenerationCommandResult>.Error("İsteğinize uygun herhangi bir resim üretilemedi", (int)HttpStatusCode.BadRequest);

            var imageDatas = result.Data
                               .Select(data => data.Base64Data)
                               .ToList();

            return Result<TextToImageGenerationCommandResult>.Success(new TextToImageGenerationCommandResult { Base64FormattedImages = imageDatas }, (int)HttpStatusCode.OK);
        }
    }
}
