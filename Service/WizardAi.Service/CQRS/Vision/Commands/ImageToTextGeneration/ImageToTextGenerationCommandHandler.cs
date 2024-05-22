using MediatR;
using OpenAI_API.Chat;
using System.Net;
using WizardAi.Core.Dto;
using WizardAi.Core.Enums;
using WizardAi.Core.Extensions;
using WizardAi.Core.External;
using WizardAi.Core.Patterns.Builder;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Vision.Commands.ImageToTextGeneration
{
    public class ImageToTextGenerationCommandHandler : IRequestHandler<ImageToTextGenerationCommand, Result<ImageToTextgenerationCommandResult>>
    {
        private readonly IOpenAiService _openAiService;

        public ImageToTextGenerationCommandHandler(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public async Task<Result<ImageToTextgenerationCommandResult>> Handle(ImageToTextGenerationCommand request, CancellationToken cancellationToken)
        {
            if(request.UrlImages.IsNullOrNotAny() && request.Files.IsNullOrNotAny())
                return Result<ImageToTextgenerationCommandResult>.Error("Bu işlem için en az 1 görsel girilmesi gerekmektedir.", (int)HttpStatusCode.BadRequest);

            ChatResult chatResult;
            if(IsSingleUrlImage(request))
            {
               chatResult = await HandleSingleUrlImageAsync(request);
            }

            else if (IsSingleFormFileImage(request))
            {
                chatResult = await HandleSingleFormFileImageAsync(request);
            }

            else
            {
                chatResult = await HandleMultipleImagesAsync(request);
            }

            var result = chatResult.Choices
                                   .Select(choice => new TextCompletionOptionDto
                                   {
                                       Message = choice.Message.TextContent,
                                       WordCount = choice.Message.TextContent.Replace("\n", "").GetWordCount()
                                   }).ToList();

            return Result<ImageToTextgenerationCommandResult>.Success(new ImageToTextgenerationCommandResult { Options = result }, (int)HttpStatusCode.OK);
        }

        #region Private Methods

        private static bool IsSingleUrlImage(ImageToTextGenerationCommand request)
        {
            return !request.UrlImages
                           .IsNullOrNotAny() && request.UrlImages
                                                       .Count == 1 && request.Files
                                                                             .IsNullOrNotAny();
        }

        private static bool IsSingleFormFileImage(ImageToTextGenerationCommand request)
        {
            return !request.Files
                           .IsNullOrNotAny() && request.Files
                                                       .Count == 1 && request.UrlImages
                                                                             .IsNullOrNotAny();
        }

        private async Task<ChatResult> HandleSingleUrlImageAsync(ImageToTextGenerationCommand request)
        {
            var singleImagePrompt = BuildSingleImagePrompt(request);
            return await _openAiService.GenerateSingleImageUrlToTextAsync(
                singleImagePrompt,
                request.UrlImages[0],
                request.CreativityType,
                request.RequestedOption);
        }

        private async Task<ChatResult> HandleSingleFormFileImageAsync(ImageToTextGenerationCommand request)
        {
            var singleImagePrompt = BuildSingleImagePrompt(request);
            var base64Image = await request.Files
                                           .ToByteArraysAsync();
            return await _openAiService.GenerateSingleBase64ImageToTextAsync(
                singleImagePrompt,
                base64Image.FirstOrDefault()
                           .Value,
                request.CreativityType,
                request.RequestedOption);
        }

        private async Task<ChatResult> HandleMultipleImagesAsync(ImageToTextGenerationCommand request)
        {
            var multipleImagesPrompt = BuildMultipleImagesPrompt(request);
            var base64Images = await request.Files
                                            .ToByteArraysAsync();
            var base64ImageList = base64Images.Select(image => image.Value)
                                              .ToList();

            return await _openAiService.GenerateMultipleImageToTextAsync(
                multipleImagesPrompt,
                request.UrlImages,
                base64ImageList,
                request.CreativityType,
                request.RequestedOption);
        }

        private static string BuildSingleImagePrompt(ImageToTextGenerationCommand request)
        {
            return new VisionPromptBuilder()
                .SetPromptType(request.PromptType)
                .SetImageLengthType(ImageLengthType.Single)
                .SetLanguageType(request.LanguageType)
                .SetDescription(request.AdditionalDescription)
                .Build();
        }

        private static string BuildMultipleImagesPrompt(ImageToTextGenerationCommand request)
        {
            return new VisionPromptBuilder()
                .SetPromptType(request.PromptType)
                .SetImageLengthType(ImageLengthType.Multiple)
                .SetLanguageType(request.LanguageType)
                .SetDescription(request.AdditionalDescription)
                .Build();
        }

        #endregion
    }
}
