using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardAi.Core.Enums;
using WizardAi.Service.Result;

namespace WizardAi.Service.CQRS.Vision.Commands.ImageToTextGeneration
{
    public class ImageToTextGenerationCommand : IRequest<Result<ImageToTextgenerationCommandResult>>
    {
        public TextPromptType PromptType { get; set; }

        public LanguageType LanguageType { get; set; }

        public CreativityType CreativityType { get; set; }

        public string? AdditionalDescription { get; set; }

        public int RequestedOption { get; set; } = 1;

        public List<string>? UrlImages { get; set; }

        [FromForm(Name = "files")]
        public IFormFileCollection Files { get; set; }
    }
}
