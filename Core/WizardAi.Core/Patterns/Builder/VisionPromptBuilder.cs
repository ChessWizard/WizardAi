using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAi.Core.Enums;
using WizardAi.Core.Extensions;

namespace WizardAi.Core.Patterns.Builder
{
    public class VisionPromptBuilder : IPromptBuilder<VisionPromptBuilder>
    {
        private TextPromptType _promptType;
        private ImageLengthType _imageLengthType;
        private LanguageType _languageType;
        private string? _description;

        public VisionPromptBuilder SetPromptType(TextPromptType promptType)
        {
            _promptType = promptType;
            return this;
        }

        public VisionPromptBuilder SetImageLengthType(ImageLengthType imageLengthType)
        {
            _imageLengthType = imageLengthType;
            return this;
        }

        public VisionPromptBuilder SetLanguageType(LanguageType languageType)
        {
            _languageType = languageType;
            return this;
        }
        
        public VisionPromptBuilder SetDescription(string? description)
        {
            _description = description;
            return this;
        }

        public string Build()
        {
            var prompt = $"{GetPromptBase()} {GetOutputLanguage()}{GetAdditionalDescription()}";
            return prompt;
        }

        #region Private Methods
        
        private string GetPromptBase()
            => $"Write an {_promptType.GetDisplayName()} content that fully reflects {GetImageContext()}, in which the last sentence must be finished meaningfully without leaving it unfinished. Have a title for the article and after writing this title, go to the bottom line and write the content.";

        private string GetImageContext()
            => _imageLengthType == ImageLengthType.Single ? "this image" : "these images";

        private string GetOutputLanguage()
            => $"Output Language: {_languageType.GetDisplayName()}";

        private string GetAdditionalDescription()
            => string.IsNullOrWhiteSpace(_description) ? "" : $"\nAdditional Description: {_description}";

        #endregion
    }
}
